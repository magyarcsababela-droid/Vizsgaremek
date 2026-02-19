using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.CONTROLLER
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            var createdOrderItem = await _orderItemService.CreateOrderItemAsync(orderItem);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = createdOrderItem.Id }, createdOrderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedOrderItem = await _orderItemService.UpdateOrderItemAsync(orderItem);
                return Ok(updatedOrderItem);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemService.DeleteOrderItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
