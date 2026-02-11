using OrderManagementSystem.Core.Entities.Enums;

namespace OrderManagementSystem.Api.ViewModels.AuthVMs.RequestVMs
{
    public class RegisterRequestViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
