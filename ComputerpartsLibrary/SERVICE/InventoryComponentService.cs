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
    public class InventoryComponentService : IInventoryComponentService
    {
        private readonly ComputerpatsDbContext _service;
        public InventoryComponentService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddInventoryComponentAsync(Inventory_components inv_comp)
        {
            await _service.Inventory_components.AddAsync(inv_comp);
            _service.SaveChanges();
        }
        public async Task DeleteInventoryComponentAsync(int id)
        {
            var entity = await _service.Inventory_components.FindAsync(id);
            if (entity != null)
            {
                _service.Inventory_components.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Inventory_components> GetInventoryComponentByIdAsync(int id)
        {
            var comp = await _service.Inventory_components.FindAsync(id);
            return comp;
        }
        public async Task<IEnumerable<Inventory_components>> GetAllInventoryComponentsAsync()
        {
            var comp = await _service.Inventory_components.ToListAsync();
            return comp;
        }
        public async Task UpdateInventoryComponentAsync(Inventory_components inv_comp)
        {
            _service.Inventory_components.Update(inv_comp);
            await _service.SaveChangesAsync();
        }
    }
}
