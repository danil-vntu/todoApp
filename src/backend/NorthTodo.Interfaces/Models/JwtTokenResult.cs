namespace NorthTodo.Interfaces.Models
{
    public class JwtTokenResult
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
