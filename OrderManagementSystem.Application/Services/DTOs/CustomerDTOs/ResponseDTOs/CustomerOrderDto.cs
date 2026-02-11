namespace OrderManagementSystem.Application.Services.DTOs.CustomerDTOs.ResponseDTOs
{
    public class CustomerOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public int ItemsCount { get; set; }
    }
}
