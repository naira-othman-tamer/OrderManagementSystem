
using OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs.ResponseDTOs;

namespace OrderManagementSystem.Api.ViewModels.InvoiceVMs
{
    public class InvoiceViewModelProfile : Profile
    {

        public InvoiceViewModelProfile()
        {
            // Response DTOs → ViewModels
            CreateMap<GetInvoiceByIdResponseDto, GetInvoiceByIdResponseViewModel>();
            CreateMap<InvoiceOrderDto, InvoiceOrderViewModel>();

            CreateMap<InvoiceDto, InvoiceViewModel>();
            CreateMap<GetAllInvoicesResponseDto, GetAllInvoicesResponseViewModel>();

            CreateMap<InvoiceFilterViewModel, InvoiceFilterDto>();
        }
    }
}
