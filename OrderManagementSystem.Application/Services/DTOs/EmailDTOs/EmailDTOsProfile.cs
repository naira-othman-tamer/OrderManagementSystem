
namespace OrderManagementSystem.Application.Services.DTOs.EmailDTOs
{
    public class EmailDTOsProfile : Profile
    {
        public EmailDTOsProfile()
        {

            CreateMap<Order, OrderStatusEmailDto>()
                .ForMember(dest => dest.CustomerEmail,
                opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NewStatus,
                opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
