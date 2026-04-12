using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryProductsController : ControllerBase
    {
        private readonly IInventoryProductService _inventoryProductService;

        public InventoryProductsController(IInventoryProductService inventoryProductService)
        {
            _inventoryProductService = inventoryProductService;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Inventory_products>> GetInventoryProductById(int productId)
        {
            var inventoryProduct = await _inventoryProductService.GetInventoryProductByIdAsync(productId);
            if (inventoryProduct == null)
                return NotFound();

            return Ok(inventoryProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory_products>>> GetAllInventoryProducts()
        {
            var inventoryProducts = await _inventoryProductService.GetAllInventoryProductsAsync();
            return Ok(inventoryProducts);
        }

        [HttpPost]
        public async Task<ActionResult> AddInventoryProduct(Inventory_products inventoryProduct)
        {
            await _inventoryProductService.AddInventoryProductAsync(inventoryProduct);
            return CreatedAtAction(nameof(GetInventoryProductById), new { productId = inventoryProduct.product_id }, inventoryProduct);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateInventoryProduct(int productId, Inventory_products inventoryProduct)
        {
            if (productId != inventoryProduct.product_id)
                return BadRequest("ID mismatch");

            await _inventoryProductService.UpdateInventoryProductAsync(inventoryProduct);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteInventoryProduct(int productId)
        {
            await _inventoryProductService.DeleteInventoryProductAsync(productId);
            return NoContent();
        }
    }

}
