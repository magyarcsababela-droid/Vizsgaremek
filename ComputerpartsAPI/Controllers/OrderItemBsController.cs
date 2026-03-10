using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemBsController : ControllerBase
    {
        private readonly IOrderItemBService _orderItemBService;

        public OrderItemBsController(IOrderItemBService orderItemBService)
        {
            _orderItemBService = orderItemBService;
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<Order_items_b>> GetOrderItemBById(int itemId)
        {
            var orderItemB = await _orderItemBService.GetOrderItemBByIdAsync(itemId);
            if (orderItemB == null)
                return NotFound();

            return Ok(orderItemB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_items_b>>> GetAllOrderItemBs()
        {
            var orderItemBs = await _orderItemBService.GetAllOrderItemBsAsync();
            return Ok(orderItemBs);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderItemB(Order_items_b orderItemB)
        {
            await _orderItemBService.AddOrderItemBAsync(orderItemB);
            return CreatedAtAction(nameof(GetOrderItemBById), new { itemId = orderItemB.item_id }, orderItemB);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult> UpdateOrderItemB(int itemId, Order_items_b orderItemB)
        {
            if (itemId != orderItemB.item_id)
                return BadRequest("ID mismatch");

            await _orderItemBService.UpdateOrderItemBAsync(orderItemB);
            return NoContent();
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteOrderItemB(int itemId)
        {
            await _orderItemBService.DeleteOrderItemBAsync(itemId);
            return NoContent();
        }

    }
}