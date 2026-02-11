
namespace OrderManagementSystem.Api.Controllers
{
    //POST /api/orders - Create a new order
    //o GET /api/orders/{orderId} - Get details of a specifi c order
    //o GET /api/orders - Get all orders (admin only)
    //o PUT /api/orders/{orderId}/status - Update order status (admin only)

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public OrdersController(IOrderService orderService, IMapper mapper, INotificationService notificationService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ResponsiveViewModel<CreateOrderResponseViewModel>> CreateOrder(CreateOrderRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<CreateOrderResponseViewModel>(
                    "Invalid input",
                    ErrorCode.ValidationError);

            var dto = _mapper.Map<CreateOrderRequestDto>(viewModel);
            var result = await _orderService.CreateOrderAsync(dto);
            if (result == null)
                return new ErrorResponseViewModel<CreateOrderResponseViewModel>(
                    "Order creation failed. Check product availability and stock",
                    ErrorCode.InvalidOrder);
            var responseViewModel = _mapper.Map<CreateOrderResponseViewModel>(result);

            return new SuccessResponseViewModel<CreateOrderResponseViewModel>(
                responseViewModel,
                "Order created successfully");
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ResponsiveViewModel<GetOrderByIdResponseViewModel>> GetOrderById(int orderId)
        {
            if (orderId <= 0)
                return new ErrorResponseViewModel<GetOrderByIdResponseViewModel>(
                    "Order Not Found",
                    ErrorCode.ValidationError);

            var result = await _orderService.GetOrderByIdAsync(orderId);

            if (result == null)
                return new ErrorResponseViewModel<GetOrderByIdResponseViewModel>(
                    "Order not found",
                    ErrorCode.OrderNotFound);

            var viewModel = _mapper.Map<GetOrderByIdResponseViewModel>(result);

            return new SuccessResponseViewModel<GetOrderByIdResponseViewModel>(viewModel, "Order retrieved successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsiveViewModel<GetAllOrdersResponseViewModel>> GetAllOrders([FromQuery] OrderFilterViewModel filter)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<GetAllOrdersResponseViewModel>(
                    "Invalid filter parameters",
                    ErrorCode.ValidationError);

            var filterDto = _mapper.Map<OrderFilterDto>(filter);

            var result = await _orderService.GetAllOrdersAsync(filterDto);
            var viewModel = _mapper.Map<GetAllOrdersResponseViewModel>(result);

            return new SuccessResponseViewModel<GetAllOrdersResponseViewModel>(viewModel, "Orders retrieved successfully");
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponsiveViewModel<UpdateOrderStatusResponseViewModel>> UpdateOrderStatus(int orderId, UpdateOrderStatusRequestViewModel viewModel)
        {
            if (!ModelState.IsValid || orderId <= 0)
                return new ErrorResponseViewModel<UpdateOrderStatusResponseViewModel>(
                    "Invalid input",
                    ErrorCode.ValidationError);

            var dto = _mapper.Map<UpdateOrderStatusRequestDto>(viewModel);
            dto.Id = orderId;

            var result = await _orderService.UpdateOrderStatusAsync(dto);

            if (result == null)
                return new ErrorResponseViewModel<UpdateOrderStatusResponseViewModel>(
                    "UpdateOrderStatusResponseViewModel",
                    ErrorCode.OrderNotFound);


            await _notificationService.NotifyOrderStatusChangedAsync(orderId, viewModel.NewStatus.ToString());


            var responseViewModel = _mapper.Map<UpdateOrderStatusResponseViewModel>(result);
            return new SuccessResponseViewModel<UpdateOrderStatusResponseViewModel>(
                responseViewModel,
                "Order status updated successfully");
        }
    }
}
