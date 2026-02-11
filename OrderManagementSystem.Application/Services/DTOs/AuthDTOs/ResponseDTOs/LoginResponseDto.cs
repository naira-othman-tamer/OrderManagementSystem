namespace OrderManagementSystem.Application.Services.DTOs.AuthDTOs.ResponseDTOs
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public int? CustomerId { get; set; }  //  Role = Customer
    }
}
