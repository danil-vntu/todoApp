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
            try
            {
                var token = await _authService.RegisterAsync(registerRequestDto);
                return Ok(new { Token = token });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginRequestDto);
                return Ok(new { Token = token });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.GetUserId();
            var result = await _authService.ChangePassword(changePasswordDto, userId);

            return Ok(result);
        }
    }
}
