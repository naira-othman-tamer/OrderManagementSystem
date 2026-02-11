namespace OrderManagementSystem.Application.Services.DTOs.CustomerDTOs.RequestDTOs
{
    public class CreateCustomerRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid? UserId { get; set; }
    }
}
