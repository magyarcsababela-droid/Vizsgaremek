using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Custom_builds>>> GetCustomBuildsByUser(int userId)
        {
            var builds = await _customBuildService.GetCustomBuildsByUserAsync(userId);
            return Ok(builds);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddCustomBuild(Custom_builds customBuild)
        {
            // set the User_id from the authenticated user
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(idClaim) && int.TryParse(idClaim, out var uid))
            {
                customBuild.User_id = uid;
            }
            else
            {
                return Unauthorized();
            }

            // enforce DB-allowed status
            if (string.IsNullOrEmpty(customBuild.status))
            {
                customBuild.status = "draft";
            }
            else
            {
                customBuild.status = customBuild.status.ToLowerInvariant();
            }

            try
            {
            await _customBuildService.AddCustomBuildAsync(customBuild);
            return CreatedAtAction(nameof(GetCustomBuildById), new { id = customBuild.build_id }, customBuild);
            }
            catch (Exception ex)
            {
                // return error details to help debugging (can be removed in production)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomBuild(int id, Custom_builds customBuild)
        {
            if (id != customBuild.build_id)
                return BadRequest("ID mismatch");

            await _customBuildService.UpdateCustomBuildAsync(customBuild);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteCustomBuild(int id)
        {
            var customBuild = await _customBuildService.GetCustomBuildByIdAsync(id);
            if (customBuild == null) return NotFound();

            // ensure the authenticated user is the owner of the build
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out var uid))
            {
                return Unauthorized();
            }

            if (customBuild.User_id != uid)
            {
                return Forbid();
            }

            await _customBuildService.DeleteCustomBuildAsync(id);
            return NoContent();
        }

    }
}
