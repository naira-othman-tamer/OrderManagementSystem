namespace OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs.ResponseDTOs
{
    public class GetInvoiceByIdResponseDto
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Order Details
        public InvoiceOrderDto Order { get; set; }
    }
}
