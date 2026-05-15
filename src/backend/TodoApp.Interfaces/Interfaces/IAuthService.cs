using TodoApp.Interfaces.DTOs.Auth;
namespace TodoApp.Interfaces.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId);
        Task<bool> CheckPasswordAsync(string password, int userId);
    }
}
