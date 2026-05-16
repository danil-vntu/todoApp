using System.ComponentModel.DataAnnotations;

namespace NorthTodo.Interfaces.DTOs.Categories
{
    public class CategoryCreateUpdateDto
    {
        [Required(ErrorMessage = "Required field!")]
        [MaxLength(100, ErrorMessage = "Category name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
