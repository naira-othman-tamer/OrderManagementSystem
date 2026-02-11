
namespace OrderManagementSystem.Application.Services.DTOs.AuthDTOs.RequestDTOs
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }  // Customer
        public Role Role { get; set; }
    }
}
