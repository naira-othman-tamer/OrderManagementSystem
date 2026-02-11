namespace OrderManagementSystem.Core.Entities
{
    //Customer: CustomerId, Name, Email, Orders
    public class Customer : BaseModel<int>
    {
        //public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();

        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
