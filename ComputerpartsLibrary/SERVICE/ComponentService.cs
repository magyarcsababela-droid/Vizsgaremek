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
    public class ComponentService : IComponentService
    {
        private readonly ComputerpatsDbContext _service;
        public ComponentService(ComputerpatsDbContext service)
        {
            _service = service;
        }

        public async Task AddComponentAsync(Components comp)
        {
            await _service.AddAsync(comp);
            await _service.SaveChangesAsync();
        }
        public async Task DeleteComponentAsync(int id)
        {
            var entity = await _service.Components.FindAsync(id);
            if (entity != null)
            {
                _service.Components.Remove(entity);
                await _service.SaveChangesAsync();
            }
        }
        public async Task<Components> GetComponentByIdAsync(int id)
        {
            var comp = await _service.Components.FindAsync(id);
            return comp;
        }
        public async Task<IEnumerable<Components>> GetAllComponentsAsync()
        {
            var comp = await _service.Components.ToListAsync();
            return comp;
        }
        public async Task UpdateComponentAsync(Components comp)
        {
            _service.Components.Update(comp);
            await _service.SaveChangesAsync();
        }
    }
}
