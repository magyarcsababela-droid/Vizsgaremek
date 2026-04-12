using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IComponentService
    {
        Task<Components> GetComponentByIdAsync(int id);
        Task<IEnumerable<Components>> GetAllComponentsAsync();
        Task AddComponentAsync(Components component);
        Task UpdateComponentAsync(Components component);
        Task DeleteComponentAsync(int id);
    }
}
