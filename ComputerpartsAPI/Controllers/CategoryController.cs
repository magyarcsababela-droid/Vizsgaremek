using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.CONTROLLER
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<categories>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<categories>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<categories>>> GetCategoriesByName(string name)
        {
            var categories = await _categoryService.GetCategoriesByNameAsync(name);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<categories>> CreateCategory(categories category)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, categories category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
                return Ok(updatedCategory);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
