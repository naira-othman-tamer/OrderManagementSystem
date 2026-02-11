

namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendOrderStatusEmailAsync(OrderStatusEmailDto emailDto);
    }
}
