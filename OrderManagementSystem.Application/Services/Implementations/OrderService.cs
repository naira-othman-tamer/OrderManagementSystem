
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        #region Create Order

        public async Task<CreateOrderResponseDto?> CreateOrderAsync(CreateOrderRequestDto request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Validate & Get Products
                var products = await ValidateAndGetProductsAsync(request.OrderItems);

                if (products == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return null;
                }

                // 2. Validate Stock
                if (!ValidateStock(request.OrderItems, products))
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return null;
                }

                // 3. Create Order Items with prices
                var orderItems = CreateOrderItems(request, products);

                // 4. Calculate totals and apply discount
                var (subtotal, totalDiscount, totalAmount) = CalculateTotals(orderItems);
                ApplyDiscountToItems(orderItems, subtotal, totalDiscount);

                // 5. Create Order
                var order = CreateOrder(request, orderItems, totalAmount);
                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                // 6. Update Stock
                await UpdateProductStockAsync(request.OrderItems);

                // 7. Generate Invoice
                await GenerateInvoiceAsync(order.Id, totalAmount);

                // 8. Final Save
                await _unitOfWork.SaveChangesAsync();

                // 9. Commit Transaction
                await _unitOfWork.CommitTransactionAsync();
                // 10. Map Response
                var response = _mapper.Map<CreateOrderResponseDto>(order);

                return response;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

        }

        #region Private Helper Methods

        private async Task<List<Product>?> ValidateAndGetProductsAsync(List<OrderItemDto> orderItems)
        {
            var productIds = orderItems.Select(x => x.ProductId).Distinct().ToList();
            var products = await _unitOfWork.ProductRepository
                .Get(p => productIds.Contains(p.Id))
                .ToListAsync();

            if (products.Count != productIds.Count)
                return null;

            return products;
        }

        private bool ValidateStock(List<OrderItemDto> orderItems, List<Product> products)
        {
            foreach (var item in orderItems)
            {
                var product = products.First(p => p.Id == item.ProductId);
                if (product.Stock < item.Quantity)
                    return false;
            }
            return true;
        }

        private List<OrderItem> CreateOrderItems(CreateOrderRequestDto request, List<Product> products)
        {
            return request.OrderItems.Select(item =>
            {
                var p = products.First(x => x.Id == item.ProductId);
                return new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = p.Price

                };
            }).ToList();
        }

        private (decimal subtotal, decimal totalDiscount, decimal totalAmount) CalculateTotals(List<OrderItem> orderItems)
        {
            decimal subtotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);
            decimal discountPercentage = GetDiscountPercentage(subtotal);
            decimal totalDiscount = subtotal * discountPercentage;
            decimal totalAmount = subtotal - totalDiscount;

            return (subtotal, totalDiscount, totalAmount);
        }

        private decimal GetDiscountPercentage(decimal subtotal)
        {
            if (subtotal > 200) return 0.10m;
            if (subtotal > 100) return 0.05m;
            return 0m;
        }

        private void ApplyDiscountToItems(List<OrderItem> orderItems, decimal subtotal, decimal totalDiscount)
        {
            foreach (var item in orderItems)
            {
                var itemSubtotal = item.UnitPrice * item.Quantity;
                item.Discount = (itemSubtotal / subtotal) * totalDiscount;
            }
        }

        private Order CreateOrder(CreateOrderRequestDto request, List<OrderItem> orderItems, decimal totalAmount)
        {
            return new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                PaymentMethod = request.PaymentMethod,
                Status = Status.Pending,
                OrderItems = orderItems
            };
        }

        private async Task UpdateProductStockAsync(List<OrderItemDto> orderItems)
        {
            foreach (var item in orderItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdWithTrackingAsync(item.ProductId);
                product.Stock -= item.Quantity;
            }
        }

        private async Task GenerateInvoiceAsync(int orderId, decimal totalAmount)
        {
            var invoice = new Invoice
            {
                OrderId = orderId,
                InvoiceDate = DateTime.Now,
                TotalAmount = totalAmount
            };
            await _unitOfWork.InvoiceRepository.AddAsync(invoice);
        }

        #endregion

        #endregion

        public async Task<GetOrderByIdResponseDto?> GetOrderByIdAsync(int orderId)
        {
            var orderDto = await _unitOfWork.OrderRepository
                .Get(o => o.Id == orderId)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ProjectTo<GetOrderByIdResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (orderDto == null) return null;

            return orderDto;
        }

        #region Get All Orders
        public async Task<List<GetOrderByIdResponseDto>> GetAllOrdersAsync(OrderFilterDto orderQuery)
        {
            var query = ApplyOrderFilter(orderQuery);
            var ordersDto = await _unitOfWork.OrderRepository
                .Get(query)
                .ProjectTo<GetOrderByIdResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ordersDto;
        }

        private Expression<Func<Order, bool>> ApplyOrderFilter(OrderFilterDto orderQuery)
        {
            var predicate = PredicateBuilder.New<Order>(true);
            if (orderQuery.Status.HasValue)
            {
                predicate = predicate.And(o => o.Status == orderQuery.Status.Value);
            }
            if (orderQuery.StartDate.HasValue)
            {
                predicate = predicate.And(o => o.OrderDate >= orderQuery.StartDate.Value);
            }
            if (orderQuery.EndDate.HasValue)
            {
                predicate = predicate.And(o => o.OrderDate <= orderQuery.EndDate.Value);
            }
            if (orderQuery.CustomerId.HasValue)
            {
                predicate = predicate.And(o => o.CustomerId == orderQuery.CustomerId.Value);
            }
            if (orderQuery.PaymentMethod.HasValue)
            {
                predicate = predicate.And(o => o.PaymentMethod == orderQuery.PaymentMethod.Value);
            }
            if (orderQuery.MinAmount.HasValue)
            {
                predicate = predicate.And(o => o.TotalAmount >= orderQuery.MinAmount.Value);
            }
            if (orderQuery.MaxAmount.HasValue)
            {
                predicate = predicate.And(o => o.TotalAmount <= orderQuery.MaxAmount.Value);
            }
            return predicate;
        }

        #endregion

        public async Task<UpdateOrderStatusResponseDto> UpdateOrderStatusAsync(UpdateOrderStatusRequestDto request)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithTrackingAsync(request.Id);

            if (order == null) return null;

            order.Status = request.NewStatus;

            await _unitOfWork.OrderRepository.UpdateIncludeAsync(order, nameof(Order.Status));
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<UpdateOrderStatusResponseDto>(order);
            return response;
        }
    }
}
