using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomBuildsController : ControllerBase
    {
        private readonly ICustomBuildService _customBuildService;

        public CustomBuildsController(ICustomBuildService customBuildService)
        {
            _customBuildService = customBuildService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Custom_builds>> GetCustomBuildById(int id)
        {
            var customBuild = await _customBuildService.GetCustomBuildByIdAsync(id);
            if (customBuild == null)
                return NotFound();

            return Ok(customBuild);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Custom_builds>>> GetAllCustomBuilds()
        {
            var customBuilds = await _customBuildService.GetAllCustomBuildsAsync();
            return Ok(customBuilds);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomBuild(Custom_builds customBuild)
        {
            await _customBuildService.AddCustomBuildAsync(customBuild);
            return CreatedAtAction(nameof(GetCustomBuildById), new { id = customBuild.BuildId }, customBuild);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomBuild(int id, Custom_builds customBuild)
        {
            if (id != customBuild.BuildId)
                return BadRequest("ID mismatch");

            await _customBuildService.UpdateCustomBuildAsync(customBuild);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomBuild(int id)
        {
            await _customBuildService.DeleteCustomBuildAsync(id);
            return NoContent();
        }

    }
}
