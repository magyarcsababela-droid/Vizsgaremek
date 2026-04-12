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
            if (user == null) throw new ArgumentNullException(nameof(user));

            // basic validation
            if (string.IsNullOrWhiteSpace(user.username)) throw new ArgumentException("Username cannot be empty", nameof(user.username));
            if (string.IsNullOrWhiteSpace(user.email)) throw new ArgumentException("Email cannot be empty", nameof(user.email));

            // prevent duplicates by username or email (case-insensitive)
            var exists = await _service.Users.AnyAsync(u => u.username.ToLower() == user.username.ToLower() || u.email.ToLower() == user.email.ToLower());
            if (exists) throw new ArgumentException("Username or email already registered");

            await _service.Users.AddAsync(user);
            await _service.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var entity = await _service.Users.FindAsync(id);
            if (entity == null) return;

            // prevent deleting Admin users
            if (!string.IsNullOrEmpty(entity.role) && string.Equals(entity.role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Cannot delete users with Admin role.");
            }

            // Remove related Addresses
            try
            {
                var addrs = _service.Addresses.Where(a => a.user_id == id).ToList();
                if (addrs.Any())
                {
                    _service.Addresses.RemoveRange(addrs);
                }
            }
            catch { }

            // Remove related custom builds
            try
            {
                var builds = _service.Custom_builds.Where(b => b.User_id == id).ToList();
                if (builds.Any())
                {
                    _service.Custom_builds.RemoveRange(builds);
                }
            }
            catch { }

            // Remove orders and their items
            try
            {
                var orders = _service.Orders.Where(o => o.user_id == id).ToList();
                foreach (var ord in orders)
                {
                    var itemsP = _service.Order_items_p.Where(i => i.order_id == ord.order_id).ToList();
                    if (itemsP.Any()) _service.Order_items_p.RemoveRange(itemsP);

                    var itemsB = _service.Order_items_b.Where(i => i.order_id == ord.order_id).ToList();
                    if (itemsB.Any()) _service.Order_items_b.RemoveRange(itemsB);

                    _service.Orders.Remove(ord);
                }
            }
            catch { }

            // Finally remove user
            _service.Users.Remove(entity);
            await _service.SaveChangesAsync();
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
            // load user from database to include full user data into token
            var user = await _service.Users.FindAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");
            return _jwtTokenService.GenerateToken(user);
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
