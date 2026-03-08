using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IInventoryComponentService
    {
        Task<Inventory_components> GetInventoryComponentByIdAsync(int componentId);
        Task<IEnumerable<Inventory_components>> GetAllInventoryComponentsAsync();
        Task AddInventoryComponentAsync(Inventory_components inventoryComponent);
        Task UpdateInventoryComponentAsync(Inventory_components inventoryComponent);
        Task DeleteInventoryComponentAsync(int componentId);
    }
}
