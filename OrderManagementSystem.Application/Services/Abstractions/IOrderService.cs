
namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface IOrderService
    {
        Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderRequestDto request);
        Task<GetOrderByIdResponseDto> GetOrderByIdAsync(int orderId);
        Task<List<GetOrderByIdResponseDto>> GetAllOrdersAsync(OrderFilterDto orderQuery);
        Task<UpdateOrderStatusResponseDto> UpdateOrderStatusAsync(UpdateOrderStatusRequestDto request);

    }
}
