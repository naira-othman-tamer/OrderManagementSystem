
namespace OrderManagementSystem.Api.ViewModels.OrderVMs.RequestVMs
{
    public class CreateOrderRequestViewModel
    {
        public int CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
