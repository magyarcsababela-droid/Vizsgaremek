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
    public class ProductService : IProductService
    {
        private readonly ComputerpatsDbContext _service;
        public ProductService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddProductAsync(Products product)
        {
            await _service.Products.AddAsync(product);
            await _service.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var entity = await _service.Products.FindAsync(id);
            if (entity != null)
            {
                _service.Products.Remove(entity);
                await _service.SaveChangesAsync();
            }
        }
        public async Task<Products> GetProductByIdAsync(int id)
        {
            var product = await _service.Products.FindAsync(id);
            return product;
        }
        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            var product = await _service.Products.ToListAsync();
            return product;
        }
        public async Task UpdateProductAsync(Products product)
        {
            _service.Products.Update(product);
            await _service.SaveChangesAsync();
        }
    }
}
