using System.ComponentModel.DataAnnotations;

namespace NorthTodo.Interfaces.DTOs.Users
{
    public class DeleteAccountRequestDto
    {
        [Required(ErrorMessage = "Required field!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        public string Password { get; set; } = null!;
    }
}
