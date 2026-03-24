using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
        public interface IUserService
    {
        Task<Users> GetUserByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
        
        /// <summary>
        /// Generates a JWT token for the given user
        /// </summary>
        Task<JwtToken> GenerateTokenAsync(int userId, string role);
        
        /// <summary>
        /// Validates a JWT token and returns the user claims
        /// </summary>
        Task<ClaimsPrincipal?> ValidateTokenAsync(string token);
    }
}
