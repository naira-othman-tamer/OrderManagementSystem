
namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request);

        Task<GetCustomerOrdersResponseDto> GetCustomerOrdersAsync(int customerId);
    }
}
