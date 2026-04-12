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
    public class OrderItemPService : IOrderItemPService
    {
        private readonly ComputerpatsDbContext _service;
        public OrderItemPService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddOrderItemPAsync(Order_items_p order_item_p)
        {
            await _service.Order_items_p.AddAsync(order_item_p);
        }
        public async Task DeleteOrderItemPAsync(int id)
        {
            var entity = await _service.Order_items_p.FindAsync(id);
            if (entity != null)
            {
                _service.Order_items_p.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Order_items_p> GetOrderItemPByIdAsync(int id)
        {
            var order_item_p = await _service.Order_items_p.FindAsync(id);
            return order_item_p;
        }
        public async Task<IEnumerable<Order_items_p>> GetAllOrderItemPsAsync()
        {
            var order_item_p = await _service.Order_items_p.ToListAsync();
            return order_item_p;
        }
        public async Task UpdateOrderItemPAsync(Order_items_p order_item_p)
        {
            _service.Order_items_p.Update(order_item_p);
            await _service.SaveChangesAsync();
        }
    }
}
