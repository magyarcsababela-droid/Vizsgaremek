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
        Task<Products> GetProductByIdAsync(int id);
        Task<IEnumerable<Products>> GetAllProductsAsync();
        Task AddProductAsync(Products product);
        Task UpdateProductAsync(Products product);
        Task DeleteProductAsync(int id);
    }
}
