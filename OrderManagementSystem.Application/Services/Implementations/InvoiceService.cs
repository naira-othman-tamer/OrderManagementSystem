
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetInvoiceByIdResponseDto?> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _unitOfWork.InvoiceRepository
                .Get(i => i.Id == invoiceId)
                .Include(i => i.Order)
                  .ThenInclude(o => o.Customer)
                .ProjectTo<GetInvoiceByIdResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (invoice == null) return null;

            GetInvoiceByIdResponseDto? responseDto = _mapper.Map<GetInvoiceByIdResponseDto>(invoice);
            return responseDto;
        }

        public async Task<GetAllInvoicesResponseDto?> GetAllInvoicesAsync(InvoiceFilterDto? filter = null)
        {
            var predicate = BuildInvoicePredicate(filter);

            var invoices = await _unitOfWork.InvoiceRepository
                .Get(predicate)
                .Include(i => i.Order)
                    .ThenInclude(o => o.Customer)
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (invoices == null || invoices.Count == 0) return null;

            return new GetAllInvoicesResponseDto { Invoices = invoices };
        }

        private Expression<Func<Invoice, bool>> BuildInvoicePredicate(InvoiceFilterDto? filter)
        {
            var predicate = PredicateBuilder.New<Invoice>(true);

            if (filter == null)
                return predicate;

            if (filter.StartDate.HasValue)
                predicate = predicate.And(i => i.InvoiceDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                predicate = predicate.And(i => i.InvoiceDate <= filter.EndDate.Value);

            if (filter.CustomerId.HasValue)
                predicate = predicate.And(i => i.Order.CustomerId == filter.CustomerId.Value);

            if (filter.MinAmount.HasValue)
                predicate = predicate.And(i => i.TotalAmount >= filter.MinAmount.Value);

            if (filter.MaxAmount.HasValue)
                predicate = predicate.And(i => i.TotalAmount <= filter.MaxAmount.Value);

            return predicate;
        }
    }
}
