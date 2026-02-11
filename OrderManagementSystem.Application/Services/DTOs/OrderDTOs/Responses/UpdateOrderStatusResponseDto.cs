namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Responses
{
    public class UpdateOrderStatusResponseDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public Status StatusEnum { get; set; }
    }
}
