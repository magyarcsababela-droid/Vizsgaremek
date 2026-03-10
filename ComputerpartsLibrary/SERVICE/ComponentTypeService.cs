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
    public class ComponentTypeService : IComponentTypeService
    {
        private readonly ComputerpatsDbContext _service;
        public ComponentTypeService(ComputerpatsDbContext service)
        {
            _service = service;
        }

        public async Task AddComponentTypeAsync(Component_type comp_type)
        {
            await _service.Component_type.AddAsync(comp_type);
            _service.SaveChanges();
        }
        public async Task DeleteComponentTypeAsync(int id)
        {
            var entity = await _service.Component_type.FindAsync(id);
            if (entity != null)
            {
                _service.Component_type.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Component_type> GetComponentTypeByIdAsync(int id)
        {
            var comp_type = await _service.Component_type.FindAsync(id);
            return comp_type;
        }
        public async Task<IEnumerable<Component_type>> GetAllComponentTypesAsync()
        {
            var comp_type = await _service.Component_type.ToListAsync();
            return comp_type;
        }
        public async Task UpdateComponentTypeAsync(Component_type comp_type)
        {
            _service.Component_type.Update(comp_type);
            await _service.SaveChangesAsync();
        }
    }
}
