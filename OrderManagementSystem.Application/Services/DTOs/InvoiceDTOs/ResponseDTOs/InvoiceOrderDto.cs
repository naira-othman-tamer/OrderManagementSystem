namespace OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs.ResponseDTOs
{
    public class InvoiceOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
