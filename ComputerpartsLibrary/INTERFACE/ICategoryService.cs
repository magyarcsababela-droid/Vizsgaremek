using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface ICategoryService
    {
        Task<categories> GetCategoryByIdAsync(int id);
        Task<IEnumerable<categories>> GetAllCategoriesAsync();
        Task<IEnumerable<categories>> GetCategoriesByNameAsync(string name);
        Task<categories> CreateCategoryAsync(categories category);
        Task<categories> UpdateCategoryAsync(categories category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
    }
}
