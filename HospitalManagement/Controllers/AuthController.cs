using HospitalManagement.Data;
using HospitalManagement.Model;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        private readonly TokenBlacklistService _blacklistService;

        public AuthController(
            AppDbContext context,
            TokenService tokenService,
            TokenBlacklistService blacklistService)
        {
            _context = context;
            _tokenService = tokenService;
            _blacklistService = blacklistService;
        }

        // REGISTER
        [HttpPost("register")]
        public IActionResult Register(User model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                return BadRequest("User already exists");

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash // simple password (no hashing)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(User model)
        {
            var user = _context.Users.FirstOrDefault(
                x => x.Email == model.Email && x.PasswordHash == model.PasswordHash);

            if (user == null)
                return Unauthorized("Invalid email or password");

            // Create JWT token
            var token = _tokenService.CreateToken(user.Id, user.Email, user.Name);

            return Ok(new
            {
                Token = token,
                User = new { user.Id, user.Name, user.Email }
            });
        }

        // LOGOUT → Add token to Redis blacklist
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            await _blacklistService.BlacklistToken(token);

            return Ok("Logged out successfully");
        }

        // CHECK USER
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok("User authenticated — token valid");
        }
    }
}
