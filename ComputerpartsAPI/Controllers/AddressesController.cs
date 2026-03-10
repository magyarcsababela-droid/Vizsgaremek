using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Addresses>> GetAddressById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addresses>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult> AddAddress(Addresses address)
        {
            await _addressService.AddAddressAsync(address);
            return CreatedAtAction(nameof(GetAddressById), new { id = address.id }, address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddress(int id, Addresses address)
        {
            if (id != address.id)
                return BadRequest("ID mismatch");

            await _addressService.UpdateAddressAsync(address);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            await _addressService.DeleteAddressAsync(id);
            return NoContent();
        }
    }

}
