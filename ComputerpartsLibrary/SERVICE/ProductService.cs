using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class ProductService : IProductService
    {
        private readonly IProductService _service;
        public ProductService(IProductService service)
        {
            _service = service;
        }
        public async Task AddProductAsync(Products product)
        {
            await _service.AddProductAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            await _service.DeleteProductAsync(id);
        }
        public async Task<Products> GetProductByIdAsync(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            return product;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            var categories = await _service.GetAllProductsAsync();
            return categories;
        }
        public async Task UpdateProductAsync(Products product)
        {
            await _service.UpdateProductAsync(product);
        }
    }
}
