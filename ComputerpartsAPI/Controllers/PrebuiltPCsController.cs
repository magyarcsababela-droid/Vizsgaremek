using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using ComputerpartsLibrary.SERVICE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrebuiltPCsController : ControllerBase
    {
        private readonly IPrebuiltPcService _prebuiltPCService;

        public PrebuiltPCsController(IPrebuiltPcService prebuiltPCService)
        {
            _prebuiltPCService = prebuiltPCService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prebuilt_pcs>> GetPrebuiltPCById(int id)
        {
            var prebuiltPC = await _prebuiltPCService.GetPrebuiltPCByIdAsync(id);
            if (prebuiltPC == null)
                return NotFound();

            return Ok(prebuiltPC);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prebuilt_pcs>>> GetAllPrebuiltPCs()
        {
            var prebuiltPCs = await _prebuiltPCService.GetAllPrebuiltPCsAsync();
            return Ok(prebuiltPCs);
        }

        [HttpPost]
        public async Task<ActionResult> AddPrebuiltPC(Prebuilt_pcs prebuiltPC)
        {
            await _prebuiltPCService.AddPrebuiltPCAsync(prebuiltPC);
            return CreatedAtAction(nameof(GetPrebuiltPCById), new { id = prebuiltPC.pc_id }, prebuiltPC);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrebuiltPC(int id, Prebuilt_pcs prebuiltPC)
        {
            if (id != prebuiltPC.pc_id)
                return BadRequest("ID mismatch");

            await _prebuiltPCService.UpdatePrebuiltPCAsync(prebuiltPC);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrebuiltPC(int id)
        {
            await _prebuiltPCService.DeletePrebuiltPCAsync(id);
            return NoContent();
        }
    }

}
