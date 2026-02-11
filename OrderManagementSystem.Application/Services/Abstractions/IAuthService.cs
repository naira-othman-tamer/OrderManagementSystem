
namespace OrderManagementSystem.Application.Services.Abstractions
{
    public interface IAuthService
    {
        Task<RegisterResponseDto?> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    }
}
