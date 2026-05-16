namespace NorthTodo.Interfaces.DTOs.Users
{
    public class UserProfileDto
    {
        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}