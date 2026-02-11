namespace OrderManagementSystem.Api.ViewModels.OrderVMs.ResponseVMs
{
    public class GetOrderByIdResponseViewModel
    {
        public int OrderId { get; set; }
        public OrderCustomerViewModel Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDetailsViewModel> OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
