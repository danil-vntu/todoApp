using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.Interfaces.DTOs.Users;
using TodoApp.Interfaces.Interfaces;

namespace TodoApp.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User is not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}