
namespace OrderManagementSystem.Application.Services.DTOs.InvoiceDTOs
{
    public class InvoiceDTOsProfile : Profile
    {
        public InvoiceDTOsProfile()
        {
            CreateMap<Invoice, GetInvoiceByIdResponseDto>()
                .ForMember(dest => dest.InvoiceId,
                opt => opt.MapFrom(src => src.Id));

            CreateMap<Order, InvoiceOrderDto>()
                .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.CustomerEmail,
                opt => opt.MapFrom(src => src.Customer.Email));

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.InvoiceId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Order.Customer.Name));
        }
    }
}
