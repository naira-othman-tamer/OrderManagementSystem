namespace OrderManagementSystem.Application.Services.DTOs.EmailDTOs
{
    public class OrderStatusEmailDto
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
}
