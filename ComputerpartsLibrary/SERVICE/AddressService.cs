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
    public class AddressService : IAddressService
    {
        private readonly ComputerpatsDbContext _service;
        public AddressService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddAddressAsync(Addresses address)
        {
            await _service.AddAsync(address);
            _service.SaveChangesAsync();
        }
        public async Task DeleteAddressAsync(int id)
        {
            var entity = await _service.Addresses.FindAsync(id);
            _service.Addresses.Remove(entity);
            _service.SaveChangesAsync();
        }
        public async Task<Addresses> GetAddressByIdAsync(int id)
        {
            var address = await _service.Addresses.FindAsync(id);
            return address;
        }
        public async Task<IEnumerable<Addresses>> GetAllAddressesAsync()
        {
            var addresses = await _service.Addresses.ToListAsync();
            return addresses;
        }
        public async Task UpdateAddressAsync(Addresses address)
        {
            _service.Addresses.Update(address);
            _service.SaveChangesAsync();
        }
    }
}

