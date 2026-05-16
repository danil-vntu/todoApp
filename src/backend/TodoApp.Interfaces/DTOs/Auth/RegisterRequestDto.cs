using System.ComponentModel.DataAnnotations;

namespace TodoApp.Interfaces.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Required field!")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(450, ErrorMessage = "Email cannot be longer than 450 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Required field!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Required field!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        public string Password { get; set; } = null!;
    }
}
