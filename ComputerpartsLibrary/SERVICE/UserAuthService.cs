using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using ComputerpartsLibrary.SERVICE;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    /// <summary>
    /// Felhasználói hitelesítési szolgáltatás bejelentkezéshez és regisztrációhoz
    /// </summary>
    public class UserAuthService
    {
        private readonly ComputerpatsDbContext _context;
        private readonly PasswordHashService _passwordHashService = new();
        
        public UserAuthService(ComputerpatsDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Új felhasználó regisztrálása jelszó hash-eléssel
        /// </summary>
        public Users RegisterUser(string username, string email, string password)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long", nameof(password));

            // Check if username or email already exists (case-insensitive)
            var usernameExists = _context.Users.Any(u => u.username.ToLower() == username.ToLower());
            if (usernameExists) throw new ArgumentException("Username already taken", nameof(username));
            var emailExists = _context.Users.Any(u => u.email.ToLower() == email.ToLower());
            if (emailExists) throw new ArgumentException("Email already registered", nameof(email));

            // Hash the password before storing
            string hashedPassword = _passwordHashService.HashPassword(password);

            var newUser = new Users
            {
                username = username,
                email = email,
                password_hash = hashedPassword,
                role = "User",
                created_at = DateTimeOffset.UtcNow
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        /// <summary>
        /// Felhasználó hitelesítése hitelesítő adatok alapján
        /// </summary>
        public Users? LoginUser(string username, string password)
        {
            var users = _context.Users.ToList();
            
            foreach (var user in users)
            {
                var loginUsername = user.username.ToLowerInvariant();
                var loginEmail = user.email.ToLowerInvariant();
                
                if ((loginUsername == username.ToLowerInvariant()) || (loginEmail == username.ToLowerInvariant()))
                {
                    // Verify password
                    if (_passwordHashService.VerifyPassword(password, user.password_hash))
                        return user;
                }
            }

            return null; // User not found or invalid credentials
        }

        /// <summary>
        /// Felhasználó szerepkörének frissítése (admin jogosultságok)
        /// </summary>
        public void UpdateUserRole(int userId, string newRole)
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.id == userId)
                {
                    user.role = newRole;
                    break;
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Felhasználói fiók törlése
        /// </summary>
        public bool DeleteUser(int userId)
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.id == userId)
                    return true;
            }
            return false; // User not found
        }
    }
}

