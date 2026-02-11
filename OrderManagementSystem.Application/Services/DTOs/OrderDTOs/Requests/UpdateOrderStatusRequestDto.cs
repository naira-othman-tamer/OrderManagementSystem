

namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Requests
{
    public class UpdateOrderStatusRequestDto
    {
        public int Id { get; set; }
        public Status NewStatus { get; set; }

    }
}
