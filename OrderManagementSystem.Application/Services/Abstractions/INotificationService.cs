namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface INotificationService
    {
        Task NotifyOrderStatusChangedAsync(int orderId, string newStatus);

    }
}
