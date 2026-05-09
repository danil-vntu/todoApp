using Microsoft.EntityFrameworkCore;
using TodoApp.Interfaces.Entities;
namespace TodoApp.Interfaces.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; } 
    DbSet<TaskItem> TaskItems { get; set; } 
    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}