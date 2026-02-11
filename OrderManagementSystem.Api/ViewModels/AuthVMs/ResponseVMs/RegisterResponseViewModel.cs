using OrderManagementSystem.Core.Entities.Enums;

namespace OrderManagementSystem.Api.ViewModels.AuthVMs.ResponseVMs
{
    public class RegisterResponseViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public int? CustomerId { get; set; }
    }
}
