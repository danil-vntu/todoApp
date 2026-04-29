using TodoApp.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}