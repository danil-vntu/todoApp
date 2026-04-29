using TodoApp.Interfaces;
namespace TodoApp.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<string> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
