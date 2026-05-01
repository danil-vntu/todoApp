namespace TodoApp.Interfaces.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
