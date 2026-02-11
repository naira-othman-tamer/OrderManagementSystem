namespace OrderManagementSystem.Api.ViewModels.InvoiceVMs.ResponseVMs
{
    public class InvoiceOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
