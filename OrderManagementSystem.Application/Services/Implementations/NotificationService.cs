
namespace OrderManagementSystem.Application.Services.Implementations
{

    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public NotificationService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task NotifyOrderStatusChangedAsync(int orderId, string newStatus)
        {
            var emailDto = await _unitOfWork.OrderRepository
                .Get(o => o.Id == orderId)
                .Select(o => new OrderStatusEmailDto
                {
                    CustomerEmail = o.Customer.Email,
                    CustomerName = o.Customer.Name,
                    OrderId = o.Id,
                    NewStatus = newStatus
                })
                .FirstOrDefaultAsync();

            if (emailDto == null)
                return;


            try
            {
                await _emailService.SendOrderStatusEmailAsync(emailDto);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Email notification failed for Order {orderId}: {ex.Message}");
            }
        }
    }
}
