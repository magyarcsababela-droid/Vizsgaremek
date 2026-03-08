using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.SERVICE
{
    public class ProductService : IProductService
    {
        private readonly ComputerpatsDbContext _context;

        public ProductService(ComputerpatsDbContext context)
        {
            _context = context;
        }

        public async Task<products> GetProductByIdAsync(int id)
        {
            return await _context.Set<products>().FindAsync(id);
        }

        public async Task<IEnumerable<products>> GetAllProductsAsync()
        {
            return await _context.Set<products>().ToListAsync();
        }

        public async Task<IEnumerable<products>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Set<products>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<products> CreateProductAsync(products product)
        {
            var newProduct = new products
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            _context.Set<products>().Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<products> UpdateProductAsync(products product)
        {
            var existingProduct = await _context.Set<products>().FindAsync(product.Id);
            if (existingProduct == null)
                throw new ArgumentException("Product not found");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Set<products>().FindAsync(id);
            if (product == null)
                return false;

            _context.Set<products>().Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
