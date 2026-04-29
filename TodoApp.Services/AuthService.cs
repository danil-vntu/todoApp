using TodoApp.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace TodoApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, 
            IPasswordHasher<User> passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto == null)
                throw new ArgumentNullException(nameof(registerRequestDto));
            if (await _userRepository.ExistsByEmailAsync(registerRequestDto.Email))
                throw new InvalidOperationException("Email already in use.");
            if (string.IsNullOrWhiteSpace(registerRequestDto.Password) 
                || registerRequestDto.Password.Length < 8)
                throw new ArgumentException("Password must be " +
                    "at least 8 characters long.");

            var user = new User 
            { 
                Email = registerRequestDto.Email, 
                Name = registerRequestDto.Name
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerRequestDto.Password);
            await _userRepository.AddAsync(user);
            return _tokenService.CreateToken(user);
        }

        public async Task<string> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return "User Logined successfully";
        }
    }
}
