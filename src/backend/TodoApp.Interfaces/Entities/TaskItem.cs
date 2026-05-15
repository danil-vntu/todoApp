namespace TodoApp.Interfaces.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateOnly? DueDate { get; set; }
        public int UserId { get; set; }
        public int? CategoryId { get; set; }
        public User User { get; set; } = null!;
        public Category? Category { get; set; } = null!;
    }
}
