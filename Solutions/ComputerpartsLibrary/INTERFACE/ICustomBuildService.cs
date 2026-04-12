using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface ICustomBuildService
    {
        Task<Custom_builds> GetCustomBuildByIdAsync(int id);
        Task<IEnumerable<Custom_builds>> GetAllCustomBuildsAsync();
        Task AddCustomBuildAsync(Custom_builds customBuild);
        Task UpdateCustomBuildAsync(Custom_builds customBuild);
        Task DeleteCustomBuildAsync(int id);
        Task<IEnumerable<Custom_builds>> GetCustomBuildsByUserAsync(int userId);
    }
}
