namespace OrderManagementSystem.Core.Entities
{
    //User: UserId, Username, PasswordHash, Role (Admin, Customer)
    public class User : BaseModel<Guid>
    {
        // public Guid UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public Customer Customer { get; set; }
    }
}
