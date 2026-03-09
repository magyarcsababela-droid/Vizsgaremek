using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class CustomBuildService : ICustomBuildService
    {
        private readonly ICustomBuildService _service;
        public CustomBuildService(ICustomBuildService service)
        {
            _service = service;
        }
        public async Task AddCustomBuildAsync(Custom_builds custom_Builds)
        {
            await _service.AddCustomBuildAsync(custom_Builds);
        }
        public async Task DeleteCustomBuildAsync(int id)
        {
            await _service.DeleteCustomBuildAsync(id);
        }
        public async Task<Custom_builds> GetCustomBuildByIdAsync(int id)
        {
            var custom_Builds = await _service.GetCustomBuildByIdAsync(id);
            return custom_Builds;
        }
        public async Task<IEnumerable<Custom_builds>> GetAllCustomBuildsAsync()
        {
            var custom_Builds = await _service.GetAllCustomBuildsAsync();
            return custom_Builds;
        }
        public async Task UpdateCustomBuildAsync(Custom_builds custom_Builds)
        {
            await _service.UpdateCustomBuildAsync(custom_Builds);
        }
    }
}
