using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class UserService : IUserService
    {
        private readonly ComputerpatsDbContext _service;
        public UserService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddUserAsync(Users user)
        {
            await _service.Users.AddAsync(user);
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
    }
}
