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
    public class PrebuiltPcCompService : IPrebuiltPcCompService
    {
        private readonly ComputerpatsDbContext _service;
        public PrebuiltPcCompService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddPrebuiltPcCompAsync(Prebuilt_pc_comp preb_pcs_comp)
        {
            await _service.Prebuilt_pc_comp.AddAsync(preb_pcs_comp);
        }
        public async Task DeletePrebuiltPcCompAsync(int pcId, int compId)
        {
            var entity = await _service.Prebuilt_pc_comp.Where(bc => bc.pc_id == pcId && bc.component_id == compId).FirstOrDefaultAsync();
            if (entity != null)
            {
                _service.Prebuilt_pc_comp.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<IEnumerable<Prebuilt_pc_comp>> GetPrebuiltPcCompByIdAsync(int pcId, int compId)
        {
            var preb_pcs_comp = await _service.Prebuilt_pc_comp.Where(bc => bc.pc_id == pcId && bc.component_id == compId).ToListAsync();
            return preb_pcs_comp;
        }
        public async Task<IEnumerable<Prebuilt_pc_comp>> GetAllPrebuiltPcCompsAsync()
        {
            var preb_pcs_comp = await _service.Prebuilt_pc_comp.ToListAsync();
            return preb_pcs_comp;
        }
        public async Task UpdatePrebuiltPcCompAsync(Prebuilt_pc_comp preb_pcs_comp)
        {
            _service.Prebuilt_pc_comp.Update(preb_pcs_comp);
            await _service.SaveChangesAsync();
        }
    }
}
