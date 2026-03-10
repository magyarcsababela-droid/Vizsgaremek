using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerpartsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemPsController : ControllerBase
    {
        private readonly IOrderItemPService _orderItemPService;

        public OrderItemPsController(IOrderItemPService orderItemPService)
        {
            _orderItemPService = orderItemPService;
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<Order_items_p>> GetOrderItemPById(int itemId)
        {
            var orderItemP = await _orderItemPService.GetOrderItemPByIdAsync(itemId);
            if (orderItemP == null)
                return NotFound();

            return Ok(orderItemP);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_items_p>>> GetAllOrderItemPs()
        {
            var orderItemPs = await _orderItemPService.GetAllOrderItemPsAsync();
            return Ok(orderItemPs);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderItemP(Order_items_p orderItemP)
        {
            await _orderItemPService.AddOrderItemPAsync(orderItemP);
            return CreatedAtAction(nameof(GetOrderItemPById), new { itemId = orderItemP.ItemId }, orderItemP);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult> UpdateOrderItemP(int itemId, Order_items_p orderItemP)
        {
            if (itemId != orderItemP.ItemId)
                return BadRequest("ID mismatch");

            await _orderItemPService.UpdateOrderItemPAsync(orderItemP);
            return NoContent();
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteOrderItemP(int itemId)
        {
            await _orderItemPService.DeleteOrderItemPAsync(itemId);
            return NoContent();
        }

    }
}