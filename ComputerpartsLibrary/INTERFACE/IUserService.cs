using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IUserService
    {
        Task<users> GetUserByIdAsync(int id);
        Task<IEnumerable<users>> GetAllUsersAsync();
        Task<users> CreateUserAsync(users user);
        Task<users> UpdateUserAsync(users user);
        Task<bool> DeleteUserAsync(int id);
    }
}
