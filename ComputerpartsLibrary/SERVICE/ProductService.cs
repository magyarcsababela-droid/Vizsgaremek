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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Set<Product>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            _context.Set<Product>().Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Set<Product>().FindAsync(product.Id);
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
            var product = await _context.Set<Product>().FindAsync(id);
            if (product == null)
                return false;

            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
