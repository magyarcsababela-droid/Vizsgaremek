using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IComponentTypeService
    {
        Task<Component_type> GetComponentTypeByIdAsync(int id);
        Task<IEnumerable<Component_type>> GetAllComponentTypesAsync();
        Task AddComponentTypeAsync(Component_type componentType);
        Task UpdateComponentTypeAsync(Component_type componentType);
        Task DeleteComponentTypeAsync(int id);
    }
}
