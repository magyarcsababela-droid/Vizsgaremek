using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class PrebuiltPcCompService : IPrebuiltPcCompService
    {
        private readonly IPrebuiltPcCompService _service;
        public PrebuiltPcCompService(IPrebuiltPcCompService service)
        {
            _service = service;
        }
        public async Task AddPrebuiltPcCompAsync(Prebuilt_pc_comp preb_pcs_comp)
        {
            await _service.AddPrebuiltPcCompAsync(preb_pcs_comp);
        }
        public async Task DeletePrebuiltPcCompAsync(int pcId, int compId)
        {
            await _service.DeletePrebuiltPcCompAsync(pcId, compId);
        }
        public async Task<Prebuilt_pc_comp> GetPrebuiltPcCompByIdAsync(int pcId, int compId)
        {
            var preb_pcs_comp = await _service.GetPrebuiltPcCompByIdAsync(pcId, compId);
            return preb_pcs_comp;
        }
        public async Task<IEnumerable<Prebuilt_pc_comp>> GetAllPrebuiltPcCompsAsync()
        {
            var preb_pcs_comp = await _service.GetAllPrebuiltPcCompsAsync();
            return preb_pcs_comp;
        }
        public async Task UpdatePrebuiltPcCompAsync(Prebuilt_pc_comp preb_pcs_comp)
        {
            await _service.UpdatePrebuiltPcCompAsync(preb_pcs_comp);
        }
    }
}
