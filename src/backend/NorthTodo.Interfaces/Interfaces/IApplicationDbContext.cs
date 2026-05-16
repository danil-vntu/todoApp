using Microsoft.EntityFrameworkCore;
using NorthTodo.Interfaces.Entities;
namespace NorthTodo.Interfaces.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; } 
    DbSet<TaskItem> TaskItems { get; set; } 
    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}