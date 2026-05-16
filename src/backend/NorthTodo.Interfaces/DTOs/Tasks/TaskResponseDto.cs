namespace NorthTodo.Interfaces.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? CategoryId { get; set; }
    }
}