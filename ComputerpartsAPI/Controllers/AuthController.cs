using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using ComputerpartsLibrary.SERVICE;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserAuthService _authService;
        private readonly ComputerpartsLibrary.SERVICE.JwtTokenService _jwtService;

        public AuthController(IUserService userService, UserAuthService authService, ComputerpartsLibrary.SERVICE.JwtTokenService jwtService)
        {
            _userService = userService;
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Visszaadja a jelenleg hitelesített felhasználó nyilvános adatait - GET /api/auth/me
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<Users>> GetCurrentUser()
        {
            // read user id from claims
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out var id))
                return Unauthorized();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var responseUser = new Users
            {
                id = user.id,
                username = user.username,
                email = user.email,
                role = user.role,
                created_at = user.created_at
            };

            return Ok(responseUser);
        }

        /// <summary>
        /// Felhasználó regisztráció végpont - POST /api/auth/register
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            try
            {
                var user = _authService.RegisterUser(
                    request.Username,
                    request.Email,
                    request.Password
                );
                // Generate JWT token for the newly created user (include full user data in token)
                var jwt = _jwtService.GenerateToken(user);

                // Build response object without sensitive fields
                var responseUser = new Users
                {
                    id = user.id,
                    username = user.username,
                    email = user.email,
                    role = user.role,
                    created_at = user.created_at
                };

                return CreatedAtAction(nameof(RegisterUser), new { id = user.id }, new { user = responseUser, token = jwt.Token, expires = jwt.Expiration });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Felhasználó bejelentkezés végpont - POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<Users>> LoginUser([FromBody] LoginRequest request)
        {
            var user = _authService.LoginUser(request.Username, request.Password);
            
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            // Generate JWT token including full user data
            var jwt = _jwtService.GenerateToken(user);

            // Remove sensitive data from response
            var responseUser = new Users
            {
                id = user.id,
                username = user.username,
                email = user.email,
                role = user.role,
                created_at = user.created_at
            };

            return Ok(new { user = responseUser, token = jwt.Token, expires = jwt.Expiration });
        }

        /// <summary>
        /// Admin végpont a felhasználó szerepkörének frissítésére - PUT /api/auth/users/{id}/role
        /// </summary>
        [HttpPut("users/{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRole(int id, [FromBody] RoleUpdateRequest request)
        {
            try
            {
                _authService.UpdateUserRole(id, request.Role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to update role" });
            }
        }

        /// <summary>
        /// Kérés modell a felhasználói regisztrációhoz
        /// </summary>
        public class RegisterRequest
        {
            [Required]
            [StringLength(50)]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(100)]
            public string Email { get; set; }

            [Required]
            [StringLength(200, MinimumLength = 8)]
            public string Password { get; set; }
        }

        /// <summary>
        /// Kérés modell a bejelentkezéshez
        /// </summary>
        public class LoginRequest
        {
            [Required]
            [StringLength(50)]
            public string Username { get; set; }

            [Required]
            [StringLength(200)]
            public string Password { get; set; }
        }

        /// <summary>
        /// Kérés modell szerepkör frissítéshez (csak admin)
        /// </summary>
        public class RoleUpdateRequest
        {
            [Required]
            public string Role { get; set; } // "Admin" or "User"
        }
    }
}
