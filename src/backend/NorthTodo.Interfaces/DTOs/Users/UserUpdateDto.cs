using System.ComponentModel.DataAnnotations;

namespace NorthTodo.Interfaces.DTOs.Users
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Required field!")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
