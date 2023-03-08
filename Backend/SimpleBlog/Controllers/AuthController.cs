using Google.Apis.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleBlog.Data.Models;
using SimpleBlog.Dtos.Requests;
using SimpleBlog.Dtos.Responses;
using SimpleBlog.Interfaces;
using SimpleBlog.Services;

namespace SimpleBlog.Controllers
{
    /// <summary>
    /// Контроллер авторизации и регистрации.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtGenerator _jwtGenerator;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _jwtGenerator = new JwtGenerator(configuration.GetValue<string>("JwtPrivateKey"));
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="dto"> Дто запроса на регистрацию. </param>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var user = new User
            {
                Email = dto.Email
            };

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
            user.Name = dto.Name;

            await _userRepository.AddUserAsync(user);

            return Ok();
        }

        /// <summary>
        /// Авторизация с помощью почты/пароля.
        /// </summary>
        /// <param name="emailLoginRequestDto"> Дто запроса на авторизацию. </param>
        /// <returns> Токен доступа и имя пользователя. </returns>
        [AllowAnonymous]
        [HttpPost("email")]
        public async Task<IActionResult> SignInWithEmail(EmailLoginRequestDto emailLoginRequestDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(emailLoginRequestDto.Email);
            if (user == null)
                return NotFound();

            var passwordHasher = new PasswordHasher<User>();

            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, emailLoginRequestDto.Password) !=
                PasswordVerificationResult.Success)
                return BadRequest();

            return Ok(new AuthResponseDto
            {
                AuthToken = _jwtGenerator.CreateAuthToken(emailLoginRequestDto.Email),
                UserName = user.Name
            });
        }

        /// <summary>
        /// Авторизация с помощью Google.
        /// </summary>
        /// <param name="dto"> Дто запроса на авторизацию. </param>
        /// <returns> Токен доступа и имя пользователя. </returns>
        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> SignInWithGoogle([FromBody] GoogleLoginRequestDto dto)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string?> { _configuration.GetValue<string>("GoogleClientId") }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);
            var user = await _userRepository.GetUserByEmailAsync(payload.Email);
            if (user == null)
                return NotFound();

            return Ok(new AuthResponseDto
            {
                AuthToken = _jwtGenerator.CreateAuthToken(payload.Email),
                UserName = user.Name
            });
        }
    }
}