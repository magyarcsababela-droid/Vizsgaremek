using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IPrebuiltPcCompService
    {
        Task<Prebuilt_pc_comp> GetPrebuiltPcCompByIdAsync(int pcId, int componentId);
        Task<IEnumerable<Prebuilt_pc_comp>> GetAllPrebuiltPcCompsAsync();
        Task AddPrebuiltPcCompAsync(Prebuilt_pc_comp prebuiltPcComp);
        Task UpdatePrebuiltPcCompAsync(Prebuilt_pc_comp prebuiltPcComp);
        Task DeletePrebuiltPcCompAsync(int pcId, int componentId);
    }
}
