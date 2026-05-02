namespace TodoApp.Interfaces.DTOs.Tasks
{
    public class TaskCreateUpdateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int CategoryId { get; set; }
    }
}