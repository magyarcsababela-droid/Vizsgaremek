using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class OrderService : IOrderService
    {
        private readonly IOrderService _service;
        public OrderService(IOrderService service)
        {
            _service = service;
        }
        public async Task AddOrderAsync(Orders order)
        {
            await _service.AddOrderAsync(order);
        }
        public async Task DeleteOrderAsync(int id)
        {
            await _service.DeleteOrderAsync(id);
        }
        public async Task<Orders> GetOrderByIdAsync(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            return order;
        }
        public async Task<IEnumerable<Orders>> GetAllOrdersAsync()
        {
            var order = await _service.GetAllOrdersAsync();
            return order;
        }
        public async Task UpdateOrderAsync(Orders order)
        {
            await _service.UpdateOrderAsync(order);
        }
    }
}
