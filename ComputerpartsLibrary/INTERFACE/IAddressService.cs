using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IAddressService
    {
        Task<Addresses> GetAddressByIdAsync(int id);
        Task<IEnumerable<Addresses>> GetAllAddressesAsync();
        Task AddAddressAsync(Addresses address);
        Task UpdateAddressAsync(Addresses address);
        Task DeleteAddressAsync(int id);

    }
}
