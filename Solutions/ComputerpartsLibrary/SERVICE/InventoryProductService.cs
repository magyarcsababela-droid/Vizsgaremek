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
    public class InventoryProductService : IInventoryProductService
    {
        private readonly ComputerpatsDbContext _service;
        public InventoryProductService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddInventoryProductAsync(Inventory_products inv_product)
        {
            await _service.Inventory_products.AddAsync(inv_product);
        }
        public async Task DeleteInventoryProductAsync(int id)
        {
            var entity = await _service.Inventory_products.FindAsync(id);
            if (entity != null)
            {
                _service.Inventory_products.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Inventory_products> GetInventoryProductByIdAsync(int id)
        {
            var inv_product = await _service.Inventory_products.FindAsync(id);
            return inv_product;
        }
        public async Task<IEnumerable<Inventory_products>> GetAllInventoryProductsAsync()
        {
            var inv_product = await _service.Inventory_products.ToListAsync();
            return inv_product;
        }
        public async Task UpdateInventoryProductAsync(Inventory_products inv_product)
        {
            _service.Inventory_products.Update(inv_product);
            await _service.SaveChangesAsync();
        }
    }
}
