using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthTodo.API.Extensions;
using NorthTodo.Interfaces.DTOs.Tasks;
using NorthTodo.Interfaces.Interfaces;

namespace NorthTodo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersTasks([FromQuery] TaskListQueryDto queryDto)
        {
            var userId = User.GetUserId();
            var tasks = await _taskService.GetUsersTasksAsync(queryDto, userId);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var userId = User.GetUserId();
            var task = await _taskService.GetTaskByIdAsync(id, userId);

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateUpdateDto taskDto)
        {
            var userId = User.GetUserId();
            var task = await _taskService.CreateTaskAsync(taskDto, userId);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskCreateUpdateDto taskDto)
        {
            var userId = User.GetUserId();
            var task = await _taskService.UpdateTaskAsync(taskDto, id, userId);

            return Ok(task);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus
            (int id, [FromBody] TaskCompletionRequestDto statusDto)
        {
            var userId = User.GetUserId();
            var task = await _taskService.ToggleTaskCompletionAsync(statusDto, id, userId);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.GetUserId();
            var result = await _taskService.DeleteTaskAsync(id, userId);

            return NoContent();
        }
    }
}