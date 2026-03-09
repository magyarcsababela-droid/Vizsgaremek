using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class InventoryProductService : IInventoryProductService
    {
        private readonly IInventoryProductService _service;
        public InventoryProductService(IInventoryProductService service)
        {
            _service = service;
        }
        public async Task AddInventoryProductAsync(Inventory_products inv_product)
        {
            await _service.AddInventoryProductAsync(inv_product);
        }
        public async Task DeleteInventoryProductAsync(int id)
        {
            await _service.DeleteInventoryProductAsync(id);
        }
        public async Task<Inventory_products> GetInventoryProductByIdAsync(int id)
        {
            var inv_product = await _service.GetInventoryProductByIdAsync(id);
            return inv_product;
        }
        public async Task<IEnumerable<Inventory_products>> GetAllInventoryProductsAsync()
        {
            var inv_product = await _service.GetAllInventoryProductsAsync();
            return inv_product;
        }
        public async Task UpdateInventoryProductAsync(Inventory_products inv_product)
        {
            await _service.UpdateInventoryProductAsync(inv_product);
        }
    }
}
