using TodoApp.Interfaces;
using TodoApp.Interfaces.DTOs;
namespace TodoApp.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
