
namespace OrderManagementSystem.Application.Services.DTOs.OrderDTOs
{
    public class OrderDTOsProfile : Profile
    {
        public OrderDTOsProfile()
        {
            // Order Mappings
            CreateMap<Order, CreateOrderResponseDto>()
                .ForMember(dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));
            //.ForMember(dest => dest.InvoiceId,
            //opt => opt.Ignore()); //  manual

            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => (src.UnitPrice * src.Quantity) - src.Discount));

            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id,
                opt => opt.Ignore())
                .ForMember(dest => dest.OrderId,
                opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice,
                opt => opt.Ignore())
                .ForMember(dest => dest.Discount,
                opt => opt.Ignore());

            #region GetOrderById

            CreateMap<Order, GetOrderByIdResponseDto>()
            .ForMember(dest => dest.OrderId,
            opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Status,
           opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Customer, OrderCustomerDto>();

            CreateMap<OrderItem, OrderItemDetailsDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => (src.UnitPrice * src.Quantity) - src.Discount));

            #endregion

            #region UpdateOrderStatus

            CreateMap<Order, UpdateOrderStatusResponseDto>()
                  .ForMember(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                 .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt ?? DateTime.Now));
            // CreateMap<UpdateOrderStatusRequestDto, Order>();

            #endregion
        }
    }
}
