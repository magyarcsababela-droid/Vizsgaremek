using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class BuildComponentService : IBuildComponentService
    {
        private readonly ComputerpatsDbContext _service;
        public BuildComponentService(ComputerpatsDbContext service)
        {
            _service = service;
        }

        public async Task AddBuildComponentAsync(Build_components build_Components)
        {
            await _service.AddAsync(build_Components);
            _service.SaveChanges();
        }
        public async Task DeleteBuildComponentAsync(int pcId, int compId)
        {
            var entity = await _service.Build_components.FindAsync(pcId, compId);
            if (entity != null) 
            {
                _service.Build_components.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Build_components> GetBuildComponentByIdAsync(int pcId, int compId)
        {
            var buildcomponent = await _service.Build_components.FindAsync(pcId, compId);
            return buildcomponent;
        }
        public async Task<IEnumerable<Build_components>> GetAllBuildComponentsAsync()
        {
            var build_Components = await _service.Build_components.ToListAsync();
            return build_Components;
        }
        public async Task UpdateBuildComponentAsync(Build_components build_Components)
        {
            _service.Build_components.Update(build_Components);
            await _service.SaveChangesAsync();
        }
    }
}
