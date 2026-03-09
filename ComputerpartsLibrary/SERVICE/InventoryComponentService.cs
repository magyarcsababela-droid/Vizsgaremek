using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class InventoryComponentService : IInventoryComponentService
    {
        private readonly IInventoryComponentService _service;
        public InventoryComponentService(IInventoryComponentService service)
        {
            _service = service;
        }
        public async Task AddInventoryComponentAsync(Inventory_components inv_comp)
        {
            await _service.AddInventoryComponentAsync(inv_comp);
        }
        public async Task DeleteInventoryComponentAsync(int id)
        {
            await _service.DeleteInventoryComponentAsync(id);
        }
        public async Task<Inventory_components> GetInventoryComponentByIdAsync(int id)
        {
            var inv_product = await _service.GetInventoryComponentByIdAsync(id);
            return inv_product;
        }
        public async Task<IEnumerable<Inventory_components>> GetAllInventoryComponentsAsync()
        {
            var categories = await _service.GetAllInventoryComponentsAsync();
            return categories;
        }
        public async Task UpdateInventoryComponentAsync(Inventory_components inv_comp)
        {
            await _service.UpdateInventoryComponentAsync(inv_comp);
        }
    }
}
