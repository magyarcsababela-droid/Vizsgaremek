using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class PrebuiltPcService : IPrebuiltPcService
    {
        private readonly IPrebuiltPcService _service;
        public PrebuiltPcService(IPrebuiltPcService service)
        {
            _service = service;
        }
        public async Task AddPrebuiltPCAsync(Prebuilt_pcs preb_pcs)
        {
            await _service.AddPrebuiltPCAsync(preb_pcs);
        }
        public async Task DeletePrebuiltPCAsync(int id)
        {
            await _service.DeletePrebuiltPCAsync(id);
        }
        public async Task<Prebuilt_pcs> GetPrebuiltPCByIdAsync(int id)
        {
            var preb_pcs = await _service.GetPrebuiltPCByIdAsync(id);
            return preb_pcs;
        }
        public async Task<IEnumerable<Prebuilt_pcs>> GetAllPrebuiltPCsAsync()
        {
            var preb_pcs = await _service.GetAllPrebuiltPCsAsync();
            return preb_pcs;
        }
        public async Task UpdatePrebuiltPCAsync(Prebuilt_pcs preb_pcs)
        {
            await _service.UpdatePrebuiltPCAsync(preb_pcs);
        }
    }
}
