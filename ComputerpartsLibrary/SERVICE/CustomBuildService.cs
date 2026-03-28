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
    public class CustomBuildService : ICustomBuildService
    {
        private readonly ComputerpatsDbContext _service;
        public CustomBuildService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddCustomBuildAsync(Custom_builds custom_Builds)
        {
            await _service.Custom_builds.AddAsync(custom_Builds);
            _service.SaveChanges();
        }
        public async Task DeleteCustomBuildAsync(int id)
        {
            var entity = await _service.Custom_builds.FindAsync(id);
            if (entity != null)
            {
                _service.Custom_builds.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Custom_builds> GetCustomBuildByIdAsync(int id)
        {
            var custom_Builds = await _service.Custom_builds.FindAsync(id);
            return custom_Builds;
        }
        public async Task<IEnumerable<Custom_builds>> GetCustomBuildsByUserAsync(int userId)
        {
            var list = await _service.Custom_builds.Where(cb => cb.User_id == userId).ToListAsync();
            return list;
        }
        public async Task<IEnumerable<Custom_builds>> GetAllCustomBuildsAsync()
        {
            var custom_Builds = await _service.Custom_builds.ToListAsync();
            return custom_Builds;
        }
        public async Task UpdateCustomBuildAsync(Custom_builds custom_Builds)
        {
            _service.Custom_builds.Update(custom_Builds);
            await _service.SaveChangesAsync();
        }
    }
}
