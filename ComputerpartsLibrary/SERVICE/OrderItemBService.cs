using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class OrderItemBService : IOrderItemBService
    {
        private readonly IOrderItemBService _service;
        public OrderItemBService(IOrderItemBService service)
        {
            _service = service;
        }
        public async Task AddOrderItemBAsync(Order_items_b order_item_b)
        {
            await _service.AddOrderItemBAsync(order_item_b);
        }
        public async Task DeleteOrderItemBAsync(int id)
        {
            await _service.DeleteOrderItemBAsync(id);
        }
        public async Task<Order_items_b> GetOrderItemBByIdAsync(int id)
        {
            var order_item_b = await _service.GetOrderItemBByIdAsync(id);
            return order_item_b;
        }
        public async Task<IEnumerable<Order_items_b>> GetAllOrderItemBsAsync()
        {
            var order_item_b = await _service.GetAllOrderItemBsAsync();
            return order_item_b;
        }
        public async Task UpdateOrderItemBAsync(Order_items_b order_item_b)
        {
            await _service.UpdateOrderItemBAsync(order_item_b);
        }
    }
}
