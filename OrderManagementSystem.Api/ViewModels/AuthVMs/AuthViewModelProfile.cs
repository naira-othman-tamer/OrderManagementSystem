
namespace OrderManagementSystem.Api.ViewModels.AuthVMs
{
    public class AuthViewModelProfile : Profile
    {
        public AuthViewModelProfile()
        {
            // Request
            CreateMap<RegisterRequestViewModel, RegisterRequestDto>();
            CreateMap<LoginRequestViewModel, LoginRequestDto>();

            // Response
            CreateMap<RegisterResponseDto, RegisterResponseViewModel>();
            CreateMap<LoginResponseDto, LoginResponseViewModel>();
        }
    }
}
