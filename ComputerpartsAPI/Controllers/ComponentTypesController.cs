using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentTypesController : ControllerBase
    {
        private readonly IComponentTypeService _componentTypeService;

        public ComponentTypesController(IComponentTypeService componentTypeService)
        {
            _componentTypeService = componentTypeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Component_type>> GetComponentTypeById(int id)
        {
            var componentType = await _componentTypeService.GetComponentTypeByIdAsync(id);
            if (componentType == null)
                return NotFound();

            return Ok(componentType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component_type>>> GetAllComponentTypes()
        {
            var componentTypes = await _componentTypeService.GetAllComponentTypesAsync();
            return Ok(componentTypes);
        }

        [HttpPost]
        public async Task<ActionResult> AddComponentType(Component_type componentType)
        {
            await _componentTypeService.AddComponentTypeAsync(componentType);
            return CreatedAtAction(nameof(GetComponentTypeById), new { id = componentType.id }, componentType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComponentType(int id, Component_type componentType)
        {
            if (id != componentType.id)
                return BadRequest("ID mismatch");

            await _componentTypeService.UpdateComponentTypeAsync(componentType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComponentType(int id)
        {
            await _componentTypeService.DeleteComponentTypeAsync(id);
            return NoContent();
        }
    }

}
