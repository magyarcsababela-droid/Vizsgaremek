using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.SERVICE
{
    public class OrderService : IOrderService
    {
        private readonly ComputerpatsDbContext _service;
        public OrderService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddOrderAsync(Orders order)
        {
            await _service.Orders.AddAsync(order);
        }
        public async Task DeleteOrderAsync(int id)
        {
            var entity = await _service.Orders.FindAsync(id);
            if (entity != null)
            {
                _service.Orders.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Orders> GetOrderByIdAsync(int id)
        {
            var order = await _service.Orders.FindAsync(id);
            return order;
        }
        public async Task<IEnumerable<Orders>> GetAllOrdersAsync()
        {
            var order = await _service.Orders.ToListAsync();
            return order;
        }
        public async Task UpdateOrderAsync(Orders order)
        {
            _service.Orders.Update(order);
            await _service.SaveChangesAsync();
        }
    }
}
