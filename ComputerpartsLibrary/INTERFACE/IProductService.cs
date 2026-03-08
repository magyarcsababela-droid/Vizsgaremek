using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IProductService
    {
        Task<products> GetProductByIdAsync(int id);
        Task<IEnumerable<products>> GetAllProductsAsync();
        Task<products> CreateProductAsync(products product);
        Task<products> UpdateProductAsync(products product);
        Task<bool> DeleteProductAsync(int id);
    }
}
