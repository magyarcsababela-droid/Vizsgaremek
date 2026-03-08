using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IInventoryProductService
    {
        Task<Inventory_products> GetInventoryProductByIdAsync(int productId);
        Task<IEnumerable<Inventory_products>> GetAllInventoryProductsAsync();
        Task AddInventoryProductAsync(Inventory_products inventoryProduct);
        Task UpdateInventoryProductAsync(Inventory_products inventoryProduct);
        Task DeleteInventoryProductAsync(int productId);
    }
}
