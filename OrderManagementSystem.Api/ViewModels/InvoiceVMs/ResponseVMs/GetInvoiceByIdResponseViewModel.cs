namespace OrderManagementSystem.Api.ViewModels.InvoiceVMs.ResponseVMs
{
    public class GetInvoiceByIdResponseViewModel
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceOrderViewModel Order { get; set; }
    }
}
