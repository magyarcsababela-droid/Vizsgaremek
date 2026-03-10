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
    public class OrderItemBService : IOrderItemBService
    {
        private readonly ComputerpatsDbContext _service;
        public OrderItemBService(ComputerpatsDbContext service)
        {
            _service = service;
        }
        public async Task AddOrderItemBAsync(Order_items_b order_item_b)
        {
            await _service.Order_items_b.AddAsync(order_item_b);
        }
        public async Task DeleteOrderItemBAsync(int id)
        {
            var entity = await _service.Order_items_b.FindAsync(id);
            if (entity != null)
            {
                _service.Order_items_b.Remove(entity);
                _service.SaveChanges();
            }
        }
        public async Task<Order_items_b> GetOrderItemBByIdAsync(int id)
        {
            var order_item_b = await _service.Order_items_b.FindAsync(id);
            return order_item_b;
        }
        public async Task<IEnumerable<Order_items_b>> GetAllOrderItemBsAsync()
        {
            var order_item_b = await _service.Order_items_b.ToListAsync();
            return order_item_b;
        }
        public async Task UpdateOrderItemBAsync(Order_items_b order_item_b)
        {
            _service.Order_items_b.Update(order_item_b);
            await _service.SaveChangesAsync();
        }
    }
}
