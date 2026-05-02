using TodoApp.Interfaces.DTOs.Tasks;
using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetUsersTasksAsync(int userId);
        Task<TaskItem?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskItem> CreateTaskAsync(TaskCreateUpdateDto taskDto, int userId);
        Task<TaskItem> UpdateTaskAsync(TaskCreateUpdateDto taskDto, int taskId, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}
