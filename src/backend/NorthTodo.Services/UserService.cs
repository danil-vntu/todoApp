using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthTodo.Interfaces.DTOs.Users;
using NorthTodo.Interfaces.Interfaces;

namespace NorthTodo.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(
            IApplicationDbContext context, 
            IMapper mapper, 
            IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<UserProfileDto?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User is not found");

            var userDto = _mapper.Map<UserProfileDto>(user);
            return userDto;
        }

        public async Task<UserProfileDto?> UpdateUserAsync(UserUpdateDto userUpdateDto, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new KeyNotFoundException("User is not found");

            _mapper.Map(userUpdateDto, user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserProfileDto>(user);
            return userDto;
        }

        public async Task DeleteUserAsync(int userId, DeleteAccountRequestDto requestDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User is not found");

            var rezult = await _authService.CheckPasswordAsync(requestDto.Password, userId);

            if (!rezult)
                throw new UnauthorizedAccessException("Invalid password.");

            user.IsDeleted = true; //soft delete
            user.DeletedAt = DateTime.UtcNow;
            user.Email = $"deleted_{Guid.NewGuid():N}_{user.Email}"; //to prevent email conflicts in the future

            await _context.SaveChangesAsync();
        }
    }
}