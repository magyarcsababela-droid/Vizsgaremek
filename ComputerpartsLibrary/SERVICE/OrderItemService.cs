using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.SERVICE
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ComputerpatsDbContext _context;

        public OrderItemService(ComputerpatsDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _context.Set<OrderItem>().FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.Set<OrderItem>().ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.Set<OrderItem>()
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            var newOrderItem = new OrderItem
            {
                Quantity = orderItem.Quantity,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId
            };

            _context.Set<OrderItem>().Add(newOrderItem);
            await _context.SaveChangesAsync();
            return newOrderItem;
        }

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var existingOrderItem = await _context.Set<OrderItem>().FindAsync(orderItem.Id);
            if (existingOrderItem == null)
                throw new ArgumentException("OrderItem not found");

            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.OrderId = orderItem.OrderId;
            existingOrderItem.ProductId = orderItem.ProductId;

            await _context.SaveChangesAsync();
            return existingOrderItem;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _context.Set<OrderItem>().FindAsync(id);
            if (orderItem == null)
                return false;

            _context.Set<OrderItem>().Remove(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
