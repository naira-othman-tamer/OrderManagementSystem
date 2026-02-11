
using OrderManagementSystem.Application.Services.DTOs.CustomerDTOs.ResponseDTOs;

namespace OrderManagementSystem.Api.ViewModels.CustomerVMs
{
    public class CustomerViewModelProfile : Profile
    {
        public CustomerViewModelProfile()
        {
            // Request ViewModels → DTOs
            CreateMap<CreateCustomerRequestViewModel, CreateCustomerRequestDto>();

            // Response DTOs → ViewModels
            CreateMap<CreateCustomerResponseDto, CreateCustomerResponseViewModel>();
            CreateMap<GetCustomerOrdersResponseDto, GetCustomerOrdersResponseViewModel>();
            CreateMap<CustomerOrderDto, CustomerOrderViewModel>();
        }
    }
}
