using ComputerpartsLibrary.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IDealService
    {
        Task<Deals> GetDealByIdAsync(int id);
        Task<IEnumerable<Deals>> GetAllDealsAsync();
        Task AddDealAsync(Deals deal);
        Task UpdateDealAsync(Deals deal);
        Task DeleteDealAsync(int id);
    }
}
