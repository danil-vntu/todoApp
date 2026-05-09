using TodoApp.Interfaces.DTOs.Users;

namespace TodoApp.Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserByIdAsync(int userId);
        Task<UserProfileDto?> UpdateUserAsync(UserUpdateDto userUpdateDto, int userId);
        Task<bool> DeleteUserAsync(int userId);
    }
}