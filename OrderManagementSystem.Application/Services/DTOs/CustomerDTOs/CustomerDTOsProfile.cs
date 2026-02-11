
namespace OrderManagementSystem.Application.Services.DTOs.CustomerDTOs
{
    public class CustomerDTOsProfile : Profile
    {
        public CustomerDTOsProfile()
        {
            // CreateCustomer Mappings
            CreateMap<CreateCustomerRequestDto, Customer>();
            CreateMap<Customer, CreateCustomerResponseDto>()
                .ForMember(dest => dest.CustomerId,
                opt => opt.MapFrom(src => src.Id));

            // GetCustomerOrders Mappings
            CreateMap<Customer, GetCustomerOrdersResponseDto>()
                .ForMember(dest => dest.CustomerId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerEmail,
                opt => opt.MapFrom(src => src.Email));

            CreateMap<Order, CustomerOrderDto>()
                .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ItemsCount,
                opt => opt.MapFrom(src => src.OrderItems.Count));
        }
    }
}
