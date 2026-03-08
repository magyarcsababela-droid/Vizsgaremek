using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class UserService : IUserService
    {
        private readonly IUserService _service;
        public UserService(IUserService service)
        {
            _service = service;
        }
        public async Task AddUserAsync(Users user)
        {
            await _service.AddUserAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _service.DeleteUserAsync(id);
        }
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            var users = await _service.GetAllUsersAsync();
            return users;
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return user;
        }
        public async Task UpdateUserAsync(Users user)
        {
            await _service.UpdateUserAsync(user);
        }
    }
}
