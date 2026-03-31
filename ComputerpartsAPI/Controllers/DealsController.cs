using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealsController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Deals>> GetDealById(int id)
        {
            var deal = await _dealService.GetDealByIdAsync(id);
            if (deal == null)
                return NotFound();

            return Ok(deal);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deals>>> GetAllDeals()
        {
            var deals = await _dealService.GetAllDealsAsync();
            return Ok(deals);
        }

        [HttpPost]
        public async Task<ActionResult> AddDeal(Deals deal)
        {
            await _dealService.AddDealAsync(deal);
            return CreatedAtAction(nameof(GetDealById), new { id = deal.id }, deal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDeal(int id, Deals deal)
        {
            if (id != deal.id)
                return BadRequest("ID mismatch");

            await _dealService.UpdateDealAsync(deal);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeal(int id)
        {
            await _dealService.DeleteDealAsync(id);
            return NoContent();
        }
    }
}
