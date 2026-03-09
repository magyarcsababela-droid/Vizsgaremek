using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class ComponentTypeService : IComponentTypeService
    {
        private readonly IComponentTypeService _service;
        public ComponentTypeService(IComponentTypeService service)
        {
            _service = service;
        }
        public async Task AddComponentTypeAsync(Component_type comp_type)
        {
            await _service.AddComponentTypeAsync(comp_type);
        }
        public async Task DeleteComponentTypeAsync(int id)
        {
            await _service.DeleteComponentTypeAsync(id);
        }
        public async Task<Component_type> GetComponentTypeByIdAsync(int id)
        {
            var inv_product = await _service.GetComponentTypeByIdAsync(id);
            return inv_product;
        }
        public async Task<IEnumerable<Component_type>> GetAllComponentTypesAsync()
        {
            var categories = await _service.GetAllComponentTypesAsync();
            return categories;
        }
        public async Task UpdateComponentTypeAsync(Component_type comp_type)
        {
            await _service.UpdateComponentTypeAsync(comp_type);
        }
    }
}
