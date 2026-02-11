namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Requests
{
    public class CreateOrderRequestDto
    {
        public int CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } //=> ProductId & Quatity
    }
}
