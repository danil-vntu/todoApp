using TodoApp.Interfaces.Models;
namespace TodoApp.Interfaces
{
    public interface ITokenService
    {
        JwtTokenResult CreateToken(User user);
    }
}