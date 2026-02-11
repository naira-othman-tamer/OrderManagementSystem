namespace OrderManagementSystem.Api.ViewModels.OrderVMs.ResponseVMs
{
    public class UpdateOrderStatusResponseViewModel
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
