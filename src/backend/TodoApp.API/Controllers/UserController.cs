using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Extensions;
using TodoApp.Interfaces.DTOs.Users;
using TodoApp.Interfaces.Interfaces;

namespace TodoApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.GetUserId();
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            var userId = User.GetUserId();
            var updatedUser = await _userService.UpdateUserAsync(userUpdateDto, userId);
            return Ok(updatedUser);
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = User.GetUserId();
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}