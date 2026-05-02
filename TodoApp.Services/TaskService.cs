using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.Interfaces.DTOs.Tasks;
using TodoApp.Interfaces.Entities;
using TodoApp.Interfaces.Interfaces;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItem>> GetUsersTasksAsync(int userId)
        {
            var tasks = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .ToListAsync();

            if (tasks == null) return Enumerable.Empty<TaskItem>();
            return tasks;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId, int userId)
        {
            var task = await _context.TaskItems.FindAsync(taskId);

            if (task == null)
                throw new KeyNotFoundException("Task is not found");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this task");

            return task;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskCreateUpdateDto taskDto, int userId)
        {
            var category = await _context.Categories.FindAsync(taskDto.CategoryId);

            if (category == null)
                throw new KeyNotFoundException("Category is not found");

            if (category.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");

            var task = _mapper.Map<TaskItem>(taskDto);
            task.UserId = userId;
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskCreateUpdateDto taskDto, int taskId, int userId)
        {
            var currentTask = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);

            if (currentTask == null)
                throw new KeyNotFoundException("Task is not found");

            if (currentTask.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this task");

            var category = await _context.Categories.FindAsync(taskDto.CategoryId);

            if (category == null)
                throw new KeyNotFoundException("Category is not found");

            if (category.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this category");

            var task = _mapper.Map(taskDto, currentTask);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _context.TaskItems.FindAsync(taskId);

            if (task == null)
                throw new KeyNotFoundException("Task is not found");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException("You do not have access to this task");

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}