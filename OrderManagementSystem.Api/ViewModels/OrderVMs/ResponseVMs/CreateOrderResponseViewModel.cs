namespace OrderManagementSystem.Api.ViewModels.OrderVMs.ResponseVMs
{
    public class CreateOrderResponseViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponseViewModel> OrderItems { get; set; }
    }
}
