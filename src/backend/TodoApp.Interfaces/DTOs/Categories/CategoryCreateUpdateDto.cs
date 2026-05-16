using System.ComponentModel.DataAnnotations;

namespace TodoApp.Interfaces.DTOs.Categories
{
    public class CategoryCreateUpdateDto
    {
        [Required(ErrorMessage = "Required field!")]
        public string Name { get; set; } = null!;
    }
}
