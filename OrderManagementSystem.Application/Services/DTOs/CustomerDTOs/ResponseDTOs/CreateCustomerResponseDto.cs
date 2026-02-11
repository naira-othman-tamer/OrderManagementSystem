namespace OrderManagementSystem.Application.Services.DTOs.CustomerDTOs.ResponseDTOs
{
    public class CreateCustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
