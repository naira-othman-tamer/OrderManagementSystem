namespace OrderManagementSystem.Api.ViewModels.OrderVMs.RequestVMs
{
    public class OrderFilterViewModel
    {
        public Status? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
