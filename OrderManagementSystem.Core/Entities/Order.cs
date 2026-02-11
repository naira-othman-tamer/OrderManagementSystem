
namespace OrderManagementSystem.Core.Entities
{
    //Order: OrderId, CustomerId, OrderDate, TotalAmount, OrderItems,PaymentMethod, Status
    public class Order : BaseModel<int>
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public PaymentMethod PaymentMethod { get; set; }
        public Status Status { get; set; }
    }
}
