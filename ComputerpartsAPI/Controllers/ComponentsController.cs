using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentsController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Components>> GetComponentById(int id)
        {
            var component = await _componentService.GetComponentByIdAsync(id);
            if (component == null)
                return NotFound();

            return Ok(component);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Components>>> GetAllComponents()
        {
            var components = await _componentService.GetAllComponentsAsync();
            return Ok(components);
        }

        [HttpPost]
        public async Task<ActionResult> AddComponent(Components component)
        {
            await _componentService.AddComponentAsync(component);
            return CreatedAtAction(nameof(GetComponentById), new { id = component.Id }, component);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComponent(int id, Components component)
        {
            if (id != component.Id)
                return BadRequest("ID mismatch");

            await _componentService.UpdateComponentAsync(component);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComponent(int id)
        {
            await _componentService.DeleteComponentAsync(id);
            return NoContent();
        }
    }

}
