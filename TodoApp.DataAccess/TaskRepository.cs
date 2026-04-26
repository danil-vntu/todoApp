using Microsoft.EntityFrameworkCore;
using TodoApp.Interfaces;

namespace TodoApp.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems
                .Include(t => t.Category)
                .Include(t => t.User)
                .ToListAsync();
        }
        public async Task<TaskItem?> GetByIdAsync(int taskId)
        {
            return await _context.TaskItems
                .Include(t => t.Category)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
        public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
        {
            return await _context.TaskItems
                .Include(t => t.Category)
                .Include(t => t.User)
                .Where(u => u.UserId == userId)
                .ToListAsync();
        }
        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<bool> DeleteAsync(int taskId)
        {
            var task = await _context.TaskItems.FindAsync(taskId);
            if (task == null) return false;
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
