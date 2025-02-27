using BCI_ASSESSET.Services;
using Microsoft.AspNetCore.Mvc;
using BCI_ASSESSET.Helper;
using BCI_ASSESSET.Response;
using BCI_ASSESSET.Repositories;
using BCI_ASSESSET.Requests.Auth;

namespace BCI_ASSESSET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly HmacHasher _hmacHasher;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(JwtService jwtService, HmacHasher hmacHasher, IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _hmacHasher = hmacHasher;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login request from user: {Username}", request.Username);

            var user = await _userRepository.getUserByUsername(request.Username);
            if (user == null)
            {
                _logger.LogWarning("Login request failed user not found with username: {Username}", request.Username);
                return new ResultActionService(false, "Invalid credentials.", null);
            }

            bool isPasswordValid = _hmacHasher.VerifyPassword(request.Password, user.Password);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Login request failed invalid password with username: {Username}", request.Username);
                return new ResultActionService(false, "Invalid credentials.", null);
            }

            var token = _jwtService.GenerateToken(user.Username);

            _logger.LogInformation("Login request successfully with username: {Username}", request.Username);
            return new ResultActionService(true, "Successfully Login the user", new AuthResponse{ Username = user.Username, Token = token });
        }
    }
}
