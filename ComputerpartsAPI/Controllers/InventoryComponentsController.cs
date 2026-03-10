using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryComponentsController : ControllerBase
    {
        private readonly IInventoryComponentService _inventoryComponentService;

        public InventoryComponentsController(IInventoryComponentService inventoryComponentService)
        {
            _inventoryComponentService = inventoryComponentService;
        }

        [HttpGet("{componentId}")]
        public async Task<ActionResult<Inventory_components>> GetInventoryComponentById(int componentId)
        {
            var inventoryComponent = await _inventoryComponentService.GetInventoryComponentByIdAsync(componentId);
            if (inventoryComponent == null)
                return NotFound();

            return Ok(inventoryComponent);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory_components>>> GetAllInventoryComponents()
        {
            var inventoryComponents = await _inventoryComponentService.GetAllInventoryComponentsAsync();
            return Ok(inventoryComponents);
        }

        [HttpPost]
        public async Task<ActionResult> AddInventoryComponent(Inventory_components inventoryComponent)
        {
            await _inventoryComponentService.AddInventoryComponentAsync(inventoryComponent);
            return CreatedAtAction(nameof(GetInventoryComponentById), new { componentId = inventoryComponent.ComponentId }, inventoryComponent);
        }

        [HttpPut("{componentId}")]
        public async Task<ActionResult> UpdateInventoryComponent(int componentId, Inventory_components inventoryComponent)
        {
            if (componentId != inventoryComponent.ComponentId)
                return BadRequest("ID mismatch");

            await _inventoryComponentService.UpdateInventoryComponentAsync(inventoryComponent);
            return NoContent();
        }

        [HttpDelete("{componentId}")]
        public async Task<ActionResult> DeleteInventoryComponent(int componentId)
        {
            await _inventoryComponentService.DeleteInventoryComponentAsync(componentId);
            return NoContent();
        }
    }

}
