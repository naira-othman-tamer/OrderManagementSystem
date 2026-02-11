namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Responses
{
    public class CreateOrderResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Status { get; set; }
        //public int InvoiceId { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; }
    }
}
