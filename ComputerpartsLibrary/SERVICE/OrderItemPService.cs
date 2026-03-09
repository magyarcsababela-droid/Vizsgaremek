using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class OrderItemPService : IOrderItemPService
    {
        private readonly IOrderItemPService _service;
        public OrderItemPService(IOrderItemPService service)
        {
            _service = service;
        }
        public async Task AddOrderItemPAsync(Order_items_p order_item_p)
        {
            await _service.AddOrderItemPAsync(order_item_p);
        }
        public async Task DeleteOrderItemPAsync(int id)
        {
            await _service.DeleteOrderItemPAsync(id);
        }
        public async Task<Order_items_p> GetOrderItemPByIdAsync(int id)
        {
            var order_item_p = await _service.GetOrderItemPByIdAsync(id);
            return order_item_p;
        }
        public async Task<IEnumerable<Order_items_p>> GetAllOrderItemPsAsync()
        {
            var order_item_p = await _service.GetAllOrderItemPsAsync();
            return order_item_p;
        }
        public async Task UpdateOrderItemPAsync(Order_items_p order_item_p)
        {
            await _service.UpdateOrderItemPAsync(order_item_p);
        }
    }
}
