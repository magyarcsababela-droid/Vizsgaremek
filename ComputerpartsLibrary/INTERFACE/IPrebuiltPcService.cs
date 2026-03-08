using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IPrebuiltPcService
    {
        Task<Prebuilt_pcs> GetPrebuiltPCByIdAsync(int id);
        Task<IEnumerable<Prebuilt_pcs>> GetAllPrebuiltPCsAsync();
        Task AddPrebuiltPCAsync(Prebuilt_pcs prebuiltPC);
        Task UpdatePrebuiltPCAsync(Prebuilt_pcs prebuiltPC);
        Task DeletePrebuiltPCAsync(int id);
    }
}
