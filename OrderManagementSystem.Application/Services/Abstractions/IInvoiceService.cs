
namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface IInvoiceService
    {
        Task<GetInvoiceByIdResponseDto?> GetInvoiceByIdAsync(int invoiceId);
        Task<GetAllInvoicesResponseDto?> GetAllInvoicesAsync(InvoiceFilterDto? filter);
    }
}
