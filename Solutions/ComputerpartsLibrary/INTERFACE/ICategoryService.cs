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
        Task<Categories> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Categories>> GetAllCategoriesAsync();
        Task AddCategoryAsync(Categories category);
        Task UpdateCategoryAsync(Categories category);
        Task DeleteCategoryAsync(int id);
    }
}
