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
    public class CategoryService : ICategoryService
    {
        private readonly ComputerpatsDbContext _service;
        public CategoryService(ComputerpatsDbContext service)
        {
            _service = service;
        }

        public async Task AddCategoryAsync(Categories category)
        {
            await _service.AddAsync(category);
            _service.SaveChanges();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var entity = await _service.Categories.FindAsync(id);
            if (entity != null)
            {
                _service.Categories.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
            var category = await _service.Categories.FindAsync(id);
            return category;
        }
        public async Task<IEnumerable<Categories>> GetAllCategoriesAsync()
        {
            var categories = await _service.Categories.ToListAsync();
            return categories;
        }
        public async Task UpdateCategoryAsync(Categories category)
        {
            _service.Categories.Update(category);
            await _service.SaveChangesAsync();
        }
    }
}
