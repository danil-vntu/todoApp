using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetUsersTasksAsync(int userId);
        Task<TaskItem?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskItem> CreateTaskAsync(TaskItem task, int userId);
        Task<TaskItem> UpdateTaskAsync(int taskId, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}
