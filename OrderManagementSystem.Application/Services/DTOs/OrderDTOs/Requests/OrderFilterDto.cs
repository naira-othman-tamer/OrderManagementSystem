

namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Requests
{
    public class OrderFilterDto
    {
        // Filter by Status
        public Status? Status { get; set; }

        // Filter by Date Range
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Filter by Customer
        public int? CustomerId { get; set; }

        // Filter by Payment Method
        public PaymentMethod? PaymentMethod { get; set; }

        // Filter by Amount Range
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
