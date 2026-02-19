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

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByNameAsync(string name)
        {
            return await _context.Set<Category>().Where(c => c.Name.Contains(name)).ToListAsync();
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var newCategory = new Category
            {
                Name = category.Name
            };
            _context.Set<Category>().Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Set<Category>().FindAsync(category.Id);
            if (existingCategory == null) { throw new ArgumentException("Category not found"); }
            existingCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Set<Category>().FindAsync(id);
            if (category == null) {  return false; }
            _context.Set<Category>().Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Set<Category>().AnyAsync(c => c.Id == id);
        }
    }
}
