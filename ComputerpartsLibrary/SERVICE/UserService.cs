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

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email
            };

            _context.Set<User>().Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Set<User>().FindAsync(user.Id);
            if (existingUser == null)
                throw new ArgumentException("User not found");

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Set<User>().FindAsync(id);
            if (user == null)
                return false;

            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
