using NorthTodo.Interfaces.DTOs.Auth;
namespace NorthTodo.Interfaces.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId);
        Task<bool> CheckPasswordAsync(string password, int userId);
    }
}
