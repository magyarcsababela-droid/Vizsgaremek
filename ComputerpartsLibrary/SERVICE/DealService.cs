using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class DealService : IDealService
    {
        private readonly ComputerpatsDbContext _service;
        public DealService(ComputerpatsDbContext service)
        {
            _service = service;
        }

        public async Task AddDealAsync(Deals deal)
        {
            await _service.Deals.AddAsync(deal);
            await _service.SaveChangesAsync();
        }

        public async Task DeleteDealAsync(int id)
        {
            var entity = await _service.Deals.FindAsync(id);
            if (entity != null)
            {
                _service.Deals.Remove(entity);
                await _service.SaveChangesAsync();
            }
        }

        public async Task<Deals> GetDealByIdAsync(int id)
        {
            var deal = await _service.Deals.FindAsync(id);
            return deal;
        }

        public async Task<IEnumerable<Deals>> GetAllDealsAsync()
        {
            var deals = await _service.Deals.ToListAsync();
            return deals;
        }

        public async Task UpdateDealAsync(Deals deal)
        {
            _service.Deals.Update(deal);
            await _service.SaveChangesAsync();
        }
    }
}
