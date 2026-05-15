using System.ComponentModel.DataAnnotations;

namespace TodoApp.Interfaces.DTOs.Tasks
{
    public class TaskCreateUpdateDto
    {
        [Required(ErrorMessage = "Required field!")]
        [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; } = null!;

        [MaxLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateOnly? DueDate { get; set; }

        public int? CategoryId { get; set; } = null!;
    }
}