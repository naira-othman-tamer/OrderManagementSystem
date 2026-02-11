namespace OrderManagementSystem.Api.ViewModels.InvoiceVMs.ResponseVMs
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
    }
}
