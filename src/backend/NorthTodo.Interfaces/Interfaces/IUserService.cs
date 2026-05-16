using NorthTodo.Interfaces.DTOs.Users;

namespace NorthTodo.Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserByIdAsync(int userId);
        Task<UserProfileDto?> UpdateUserAsync(UserUpdateDto userUpdateDto, int userId);
        Task DeleteUserAsync(int userId, DeleteAccountRequestDto requestDto);
    }
}