using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.SERVICE
{
    public class UserService : IUserService
    {
        private readonly ComputerpatsDbContext _context;

        public UserService(ComputerpatsDbContext context)
        {
            _context = context;
        }

        public async Task<users> GetUserByIdAsync(int id)
        {
            return await _context.Set<users>().FindAsync(id);
        }

        public async Task<IEnumerable<users>> GetAllUsersAsync()
        {
            return await _context.Set<users>().ToListAsync();
        }

        public async Task<users> CreateUserAsync(users user)
        {
            var newUser = new users
            {
                UserName = user.UserName,
                Email = user.Email
            };

            _context.Set<users>().Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<users> UpdateUserAsync(users user)
        {
            var existingUser = await _context.Set<users>().FindAsync(user.Id);
            if (existingUser == null)
                throw new ArgumentException("User not found");

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Set<users>().FindAsync(id);
            if (user == null)
                return false;

            _context.Set<users>().Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
