using Microsoft.AspNetCore.Identity;
using NorthTodo.Interfaces.DTOs.Auth;
using NorthTodo.Interfaces.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthTodo.Interfaces.Interfaces;
namespace NorthTodo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IApplicationDbContext context,
            IPasswordHasher<User> passwordHasher, ITokenService tokenService,
            IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        private AuthResponseDto CreateAuthResponse(User user)
        {
            var tokenResult = _tokenService.CreateToken(user);

            return _mapper.Map<AuthResponseDto>(tokenResult);
        }

        private async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        private async Task<User?> GetUserByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        private async Task<bool> ExistsByEmailAsync(string email) =>
            await _context.Users.AnyAsync(u => u.Email == email);

        private async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto == null)
                throw new ArgumentNullException(nameof(registerRequestDto));
            if (await ExistsByEmailAsync(registerRequestDto.Email))
                throw new InvalidOperationException("Email already in use.");
            if (string.IsNullOrWhiteSpace(registerRequestDto.Password)
                || registerRequestDto.Password.Length < 8)
                throw new ArgumentException("Password must be " +
                    "at least 8 characters long.");

            var user = _mapper.Map<User>(registerRequestDto);

            user.PasswordHash = _passwordHasher.HashPassword(user, registerRequestDto.Password);

            await AddAsync(user);

            return CreateAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto == null)
                throw new ArgumentNullException(nameof(loginRequestDto));
            var user = await GetUserByEmailAsync(loginRequestDto.Email);
            if (user == null)
                throw new KeyNotFoundException("User is not found.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequestDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid email or password.");

            return CreateAuthResponse(user);
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto, int userId)
        {
            if (changePasswordDto == null)
                throw new ArgumentNullException(nameof(changePasswordDto));

            var user = await GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User is not found.");

            if (_passwordHasher.VerifyHashedPassword
                (user, user.PasswordHash, changePasswordDto.OldPassword) ==
                PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid email or password.");

            user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
            await _context.SaveChangesAsync();
            return "Password changed successfully.";
        }

        public async Task<bool> CheckPasswordAsync(string password, int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User is not found.");

            if (_passwordHasher.VerifyHashedPassword
                (user, user.PasswordHash, password) ==
                PasswordVerificationResult.Failed)
                return false;

            return true;
        }
    }
}
