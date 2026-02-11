
namespace OrderManagementSystem.Application.Services.DTOs.AuthDTOs
{
    public class AuthDTOsProfile : Profile
    {
        public AuthDTOsProfile()
        {
            // Register
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Username,
                opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore()); //  Manual

            CreateMap<RegisterRequestDto, CreateCustomerRequestDto>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId,
                opt => opt.Ignore()); //  Manual
        }
    }
}
