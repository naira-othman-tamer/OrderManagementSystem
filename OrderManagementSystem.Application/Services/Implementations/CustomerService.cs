
namespace OrderManagementSystem.Application.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateCustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request)
        {
            var newCustomer = _mapper.Map<Customer>(request);
            await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
            await _unitOfWork.SaveChangesAsync();
            CreateCustomerResponseDto? responseDto = _mapper.Map<CreateCustomerResponseDto>(newCustomer);
            return responseDto;
        }

        public async Task<GetCustomerOrdersResponseDto?> GetCustomerOrdersAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomerRepository
                .Get(c => c.Id == customerId)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                //  .ProjectTo<GetCustomerOrdersResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (customer == null)
                return null;

            GetCustomerOrdersResponseDto? response = _mapper.Map<GetCustomerOrdersResponseDto>(customer);
            return response;
        }
    }
}
