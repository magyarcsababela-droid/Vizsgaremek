using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class AddressService : IAddressService
    {
        private readonly IAddressService _service;
        public AddressService(IAddressService service)
        {
            _service = service;
        }
        public async Task AddAddressAsync(Addresses address)
        {
            await _service.AddAddressAsync(address);
        }
        public async Task DeleteAddressAsync(int id)
        {
            await _service.DeleteAddressAsync(id);
        }
        public async Task<Addresses> GetAddressByIdAsync(int id)
        {
            var address = await _service.GetAddressByIdAsync(id);
            return address;
        }
        public async Task<IEnumerable<Addresses>> GetAllAddressesAsync()
        {
            var addresses = await _service.GetAllAddressesAsync();
            return addresses;
        }
        public async Task UpdateAddressAsync(Addresses address)
        {
            await _service.UpdateAddressAsync(address);
        }
    }
}
