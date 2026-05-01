using TodoApp.Interfaces.Entities;

namespace TodoApp.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int userId);
    }
}
