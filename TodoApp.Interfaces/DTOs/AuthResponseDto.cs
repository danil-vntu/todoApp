namespace TodoApp.Interfaces.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
