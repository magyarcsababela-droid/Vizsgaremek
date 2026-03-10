using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrebuiltPcCompsController : ControllerBase
    {
        private readonly IPrebuiltPcCompService _prebuiltPcCompService;

        public PrebuiltPcCompsController(IPrebuiltPcCompService prebuiltPcCompService)
        {
            _prebuiltPcCompService = prebuiltPcCompService;
        }

        [HttpGet("{pcId}/{componentId}")]
        public async Task<ActionResult<Prebuilt_pc_comp>> GetPrebuiltPcCompById(int pcId, int componentId)
        {
            var prebuiltPcComp = await _prebuiltPcCompService.GetPrebuiltPcCompByIdAsync(pcId, componentId);
            if (prebuiltPcComp == null)
                return NotFound();

            return Ok(prebuiltPcComp);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prebuilt_pc_comp>>> GetAllPrebuiltPcComps()
        {
            var prebuiltPcComps = await _prebuiltPcCompService.GetAllPrebuiltPcCompsAsync();
            return Ok(prebuiltPcComps);
        }

        [HttpPost]
        public async Task<ActionResult> AddPrebuiltPcComp(Prebuilt_pc_comp prebuiltPcComp)
        {
            await _prebuiltPcCompService.AddPrebuiltPcCompAsync(prebuiltPcComp);
            return CreatedAtAction(nameof(GetPrebuiltPcCompById), new { pcId = prebuiltPcComp.PcId, componentId = prebuiltPcComp.ComponentId }, prebuiltPcComp);
        }

        [HttpPut("{pcId}/{componentId}")]
        public async Task<ActionResult> UpdatePrebuiltPcComp(int pcId, int componentId, Prebuilt_pc_comp prebuiltPcComp)
        {
            if (pcId != prebuiltPcComp.PcId || componentId != prebuiltPcComp.ComponentId)
                return BadRequest("ID mismatch");

            await _prebuiltPcCompService.UpdatePrebuiltPcCompAsync(prebuiltPcComp);
            return NoContent();
        }

        [HttpDelete("{pcId}/{componentId}")]
        public async Task<ActionResult> DeletePrebuiltPcComp(int pcId, int componentId)
        {
            await _prebuiltPcCompService.DeletePrebuiltPcCompAsync(pcId, componentId);
            return NoContent();
        }
    }

}
