using NorthTodo.Interfaces.Entities;
using NorthTodo.Interfaces.Models;
namespace NorthTodo.Interfaces.Interfaces
{
    public interface ITokenService
    {
        JwtTokenResult CreateToken(User user);
    }
}