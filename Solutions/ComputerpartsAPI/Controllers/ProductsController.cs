using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Products product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.id)
                return BadRequest("ID mismatch");

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }

}
