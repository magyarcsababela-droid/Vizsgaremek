using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class UserService : IUserService
    {
        private readonly ComputerpatsDbContext _service;
        private readonly JwtTokenService _jwtTokenService;

        public UserService(ComputerpatsDbContext service, JwtTokenService jwtTokenService)
        {
            _service = service;
            _jwtTokenService = jwtTokenService;
        }
        public async Task AddUserAsync(Users user)
        {
            await _service.Users.AddAsync(user);
            _service.SaveChanges();
        }
        public async Task DeleteUserAsync(int id)
        {
            var entity = await _service.Users.FindAsync(id);
            if (entity != null)
            {
                _service.Users.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            var user = await _service.Users.FindAsync(id);
            return user;
        }
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            var users = await _service.Users.ToListAsync();
            return users;
        }
        public async Task UpdateUserAsync(Users user)
        {
            _service.Users.Update(user);
            await _service.SaveChangesAsync();
        }

        /// <summary>
        /// Generates a JWT token for the given user
        /// </summary>
        public async Task<JwtToken> GenerateTokenAsync(int userId, string role)
        {
            return _jwtTokenService.GenerateToken(userId, role);
        }

        /// <summary>
        /// Validates a JWT token and returns the user claims
        /// </summary>
        public async Task<ClaimsPrincipal?> ValidateTokenAsync(string token)
        {
            try
            {
                return _jwtTokenService.ValidateToken(token);
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }
    }
}
