using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using TodoApp.Interfaces.DTOs.Auth;
using TodoApp.Interfaces.Interfaces;
using TodoApp.API.Extensions;
namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var token = await _authService.RegisterAsync(registerRequestDto);
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var token = await _authService.LoginAsync(loginRequestDto);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.GetUserId();
            var result = await _authService.ChangePasswordAsync(changePasswordDto, userId);

            return Ok(result);
        }
    }
}
