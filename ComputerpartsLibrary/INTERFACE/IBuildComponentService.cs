using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IBuildComponentService
    {
        Task<Build_components> GetBuildComponentByIdAsync(int buildId, int componentId);
        Task<IEnumerable<Build_components>> GetAllBuildComponentsAsync();
        Task AddBuildComponentAsync(Build_components buildComponent);
        Task UpdateBuildComponentAsync(Build_components buildComponent);
        Task DeleteBuildComponentAsync(int buildId, int componentId);
    }
}
