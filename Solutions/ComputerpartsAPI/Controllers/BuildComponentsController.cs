using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildComponentsController : ControllerBase
    {
        private readonly IBuildComponentService _buildComponentService;

        public BuildComponentsController(IBuildComponentService buildComponentService)
        {
            _buildComponentService = buildComponentService;
        }

        [HttpGet("{buildId}/{componentId}")]
        public async Task<ActionResult<Build_components>> GetBuildComponentById(int buildId, int componentId)
        {
            var buildComponent = await _buildComponentService.GetBuildComponentByIdAsync(buildId, componentId);
            if (buildComponent == null)
                return NotFound();

            return Ok(buildComponent);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Build_components>>> GetAllBuildComponents()
        {
            var buildComponents = await _buildComponentService.GetAllBuildComponentsAsync();
            return Ok(buildComponents);
        }

        [HttpPost]
        public async Task<ActionResult> AddBuildComponent(Build_components buildComponent)
        {
            await _buildComponentService.AddBuildComponentAsync(buildComponent);
            return CreatedAtAction(nameof(GetBuildComponentById), new { buildId = buildComponent.build_id, componentId = buildComponent.component_id }, buildComponent);
        }

        [HttpPut("{buildId}/{componentId}")]
        public async Task<ActionResult> UpdateBuildComponent(int buildId, int componentId, Build_components buildComponent)
        {
            if (buildId != buildComponent.build_id || componentId != buildComponent.component_id)
                return BadRequest("ID mismatch");

            await _buildComponentService.UpdateBuildComponentAsync(buildComponent);
            return NoContent();
        }

        [HttpDelete("{buildId}/{componentId}")]
        public async Task<ActionResult> DeleteBuildComponent(int buildId, int componentId)
        {
            await _buildComponentService.DeleteBuildComponentAsync(buildId, componentId);
            return NoContent();
        }

    }
}