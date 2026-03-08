using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.SERVICE
{
    public class CategoryService : ICategoryService
    {
        private readonly ComputerpatsDbContext _context;

        public CategoryService(ComputerpatsDbContext context)
        {
            _context = context;
        }

        public async Task<categories> GetCategoryByIdAsync(int id)
        {
            return await _context.Set<categories>().FindAsync(id);
        }

        public async Task<IEnumerable<categories>> GetAllCategoriesAsync()
        {
            return await _context.Set<categories>().ToListAsync();
        }

        public async Task<IEnumerable<categories>> GetCategoriesByNameAsync(string name)
        {
            return await _context.Set<categories>().Where(c => c.Name.Contains(name)).ToListAsync();
        }
        public async Task<categories> CreateCategoryAsync(categories category)
        {
            var newCategory = new categories
            {
                Name = category.Name
            };
            _context.Set<categories>().Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<categories> UpdateCategoryAsync(categories category)
        {
            var existingCategory = await _context.Set<categories>().FindAsync(category.Id);
            if (existingCategory == null) { throw new ArgumentException("Category not found"); }
            existingCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Set<categories>().FindAsync(id);
            if (category == null) {  return false; }
            _context.Set<categories>().Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Set<categories>().AnyAsync(c => c.Id == id);
        }
    }
}
