using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class BuildComponentService : IBuildComponentService
    {
        private readonly IBuildComponentService _service;
        public BuildComponentService(IBuildComponentService service)
        {
            _service = service;
        }
        public async Task AddBuildComponentAsync(Build_components build_Components)
        {
            await _service.AddBuildComponentAsync(build_Components);
        }
        public async Task DeleteBuildComponentAsync(int pcId, int compId)
        {
            await _service.DeleteBuildComponentAsync(pcId, compId);
        }
        public async Task<Build_components> GetBuildComponentByIdAsync(int pcId, int compId)
        {
            var build_Components = await _service.GetBuildComponentByIdAsync(pcId, compId);
            return build_Components;
        }
        public async Task<IEnumerable<Build_components>> GetAllBuildComponentsAsync()
        {
            var build_Components = await _service.GetAllBuildComponentsAsync();
            return build_Components;
        }
        public async Task UpdateBuildComponentAsync(Build_components build_Components)
        {
            await _service.UpdateBuildComponentAsync(build_Components);
        }
    }
}
