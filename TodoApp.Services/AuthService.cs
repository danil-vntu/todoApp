using TodoApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using TodoApp.Interfaces.DTOs;
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

        private AuthResponseDto CreateAuthResponse(User user)
        {
            var tokenResult = _tokenService.CreateToken(user);

            return new AuthResponseDto
            {
                Token = tokenResult.Token,
                Expiration = tokenResult.Expiration
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
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

            return CreateAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto == null)
                throw new ArgumentNullException(nameof(loginRequestDto));
            var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email);
            if (user == null)
                throw new InvalidOperationException("Invalid email or password.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequestDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid email or password.");

            return CreateAuthResponse(user);
        }
    }
}
