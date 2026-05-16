using System.ComponentModel.DataAnnotations;

namespace TodoApp.Interfaces.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Required field!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Required field!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        public string NewPassword { get; set; } = null!;
    }
}
