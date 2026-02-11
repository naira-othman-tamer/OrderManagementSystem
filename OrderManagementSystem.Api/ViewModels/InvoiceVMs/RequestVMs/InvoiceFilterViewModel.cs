namespace OrderManagementSystem.Api.ViewModels.InvoiceVMs.RequestVMs
{
    public class InvoiceFilterViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
