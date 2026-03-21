using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using ComputerpartsLibrary.SERVICE;
using Microsoft.AspNetCore.Authorization;
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

        public AuthController(IUserService userService, UserAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// User registration endpoint - POST /api/auth/register
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<Users>> RegisterUser([FromBody] RegisterRequest request)
        {
            try
            {
                var user = _authService.RegisterUser(
                    request.Username,
                    request.Email,
                    request.Password
                );
                return CreatedAtAction(nameof(RegisterUser), new { id = user.id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// User login endpoint - POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<Users>> LoginUser([FromBody] LoginRequest request)
        {
            var user = _authService.LoginUser(request.Username, request.Password);
            
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            // Remove sensitive data from response
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
        /// Admin endpoint to update user role - PUT /api/auth/users/{id}/role
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
        /// Request model for user registration
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
        /// Request model for user login
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
        /// Request model for role update (admin only)
        /// </summary>
        public class RoleUpdateRequest
        {
            [Required]
            public string Role { get; set; } // "Admin" or "User"
        }
    }
}
