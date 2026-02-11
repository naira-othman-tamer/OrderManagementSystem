namespace OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs.RequestDTOs
{
    public class InvoiceFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
