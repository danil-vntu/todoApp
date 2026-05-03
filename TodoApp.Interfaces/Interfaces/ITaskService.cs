using TodoApp.Interfaces.DTOs.Paging;
using TodoApp.Interfaces.DTOs.Tasks;
using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResultDto<TaskResponseDto>> GetUsersTasksAsync(TaskListQueryDto queryDto , int userId);
        Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskResponseDto> CreateTaskAsync(TaskCreateUpdateDto taskDto, int userId);
        Task<TaskResponseDto> UpdateTaskAsync(TaskCreateUpdateDto taskDto, int taskId, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}
