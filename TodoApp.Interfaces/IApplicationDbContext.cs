using Microsoft.EntityFrameworkCore;
namespace TodoApp.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; } 
    DbSet<TaskItem> TaskItems { get; set; } 
    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}