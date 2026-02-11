namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Responses
{
    public class GetOrderByIdResponseDto
    {
        public int OrderId { get; set; }
        public OrderCustomerDto Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDetailsDto> OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
