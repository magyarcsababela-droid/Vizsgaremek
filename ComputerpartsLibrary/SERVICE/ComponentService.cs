using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentService _service;
        public ComponentService(IComponentService service)
        {
            _service = service;
        }
        public async Task AddComponentAsync(Components comp)
        {
            await _service.AddComponentAsync(comp);
        }
        public async Task DeleteComponentAsync(int id)
        {
            await _service.DeleteComponentAsync(id);
        }
        public async Task<Components> GetComponentByIdAsync(int id)
        {
            var comp = await _service.GetComponentByIdAsync(id);
            return comp;
        }
        public async Task<IEnumerable<Components>> GetAllComponentsAsync()
        {
            var comp = await _service.GetAllComponentsAsync();
            return comp;
        }
        public async Task UpdateComponentAsync(Components comp)
        {
            await _service.UpdateComponentAsync(comp);
        }
    }
}
