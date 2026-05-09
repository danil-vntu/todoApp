using TodoApp.Interfaces.DTOs.Auth;
namespace TodoApp.Interfaces.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<string> ChangePassword(ChangePasswordDto changePasswordDto, int userId);
    }
}
