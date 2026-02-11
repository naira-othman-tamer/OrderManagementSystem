
using OrderManagementSystem.Application.Services.DTOs.OrderDTOs.Responses;

namespace OrderManagementSystem.Api.ViewModels.OrderVMs
{
    public class OrderViewModelProfile : Profile
    {
        public OrderViewModelProfile()
        {
            // Request ViewModels → DTOs
            CreateMap<CreateOrderRequestViewModel, CreateOrderRequestDto>();

            CreateMap<OrderItemViewModel, OrderItemDto>();

            CreateMap<UpdateOrderStatusRequestViewModel, UpdateOrderStatusRequestDto>();

            CreateMap<OrderFilterViewModel, OrderFilterDto>();

            // Response DTOs → ViewModels
            CreateMap<CreateOrderResponseDto, CreateOrderResponseViewModel>();

            CreateMap<OrderItemResponseDto, OrderItemResponseViewModel>();

            CreateMap<GetOrderByIdResponseDto, GetOrderByIdResponseViewModel>()
                .ForMember(dest => dest.OrderId,
                opt =>
                opt.MapFrom(src => src.OrderId));

            CreateMap<OrderCustomerDto, OrderCustomerViewModel>();

            CreateMap<OrderItemDetailsDto, OrderItemDetailsViewModel>();

            CreateMap<GetOrderByIdResponseDto, OrderSummaryViewModel>()
                .ForMember(dest => dest.OrderId,
                opt =>
                opt.MapFrom(src => src.OrderId));

            CreateMap<List<GetOrderByIdResponseDto>, GetAllOrdersResponseViewModel>()
                .ForMember(dest => dest.Orders,
                opt =>
                opt.MapFrom(src => src));

            CreateMap<UpdateOrderStatusResponseDto, UpdateOrderStatusResponseViewModel>();
        }
    }
}
