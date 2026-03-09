using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService _service;
        public CategoryService(ICategoryService service)
        {
            _service = service;
        }
        public async Task AddCategoryAsync(Categories category)
        {
            await _service.AddCategoryAsync(category);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _service.DeleteCategoryAsync(id);
        }
        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            return category;
        }
        public async Task<IEnumerable<Categories>> GetAllCategoriesAsync()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return categories;
        }
        public async Task UpdateCategoryAsync(Categories category)
        {
            await _service.UpdateCategoryAsync(category);
        }
    }
}
