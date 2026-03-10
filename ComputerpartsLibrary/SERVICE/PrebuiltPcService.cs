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
    public class PrebuiltPcService : IPrebuiltPcService
    {
        private readonly ComputerpatsDbContext _service;
        public PrebuiltPcService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddPrebuiltPCAsync(Prebuilt_pcs preb_pcs)
        {
            await _service.Prebuilt_pcs.AddAsync(preb_pcs);
        }
        public async Task DeletePrebuiltPCAsync(int id)
        {
            var entity = await _service.Prebuilt_pcs.FindAsync(id);
            if (entity != null)
            {
                _service.Prebuilt_pcs.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Prebuilt_pcs> GetPrebuiltPCByIdAsync(int id)
        {
            var preb_pcs = await _service.Prebuilt_pcs.FindAsync(id);
            return preb_pcs;
        }
        public async Task<IEnumerable<Prebuilt_pcs>> GetAllPrebuiltPCsAsync()
        {
            var preb_pcs = await _service.Prebuilt_pcs.ToListAsync();
            return preb_pcs;
        }
        public async Task UpdatePrebuiltPCAsync(Prebuilt_pcs preb_pcs)
        {
            _service.Prebuilt_pcs.Update(preb_pcs);
            await _service.SaveChangesAsync();
        }
    }
}
