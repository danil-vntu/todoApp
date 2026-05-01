using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int taskId);
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
        Task<TaskItem> AddAsync(TaskItem task); 
        Task<TaskItem> UpdateAsync(TaskItem task); 
        Task<bool> DeleteAsync(int taskId);
    }
}
