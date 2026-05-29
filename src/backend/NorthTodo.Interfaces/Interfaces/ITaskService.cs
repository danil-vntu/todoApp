using NorthTodo.Interfaces.DTOs.Paging;
using NorthTodo.Interfaces.DTOs.Tasks;
using NorthTodo.Interfaces.Entities;

namespace NorthTodo.Interfaces.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResultDto<TaskResponseDto>> GetUsersTasksAsync(TaskListQueryDto queryDto , int userId);
        Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskResponseDto> CreateTaskAsync(TaskCreateUpdateDto taskDto, int userId);
        Task<TaskResponseDto> UpdateTaskAsync(TaskCreateUpdateDto taskDto, int taskId, int userId);
        Task<TaskResponseDto> ToggleTaskCompletionAsync
            (TaskCompletionRequestDto statusUpdateDto, int taskId, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}
