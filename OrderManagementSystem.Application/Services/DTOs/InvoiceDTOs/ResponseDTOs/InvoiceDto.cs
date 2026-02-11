namespace OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs.ResponseDTOs
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
    }
}
