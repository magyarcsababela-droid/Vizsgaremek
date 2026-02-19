using Microsoft.EntityFrameworkCore;
using ComputerpartsLibrary.DATA;
using ComputerpartsLibrary.INTERFACE;
using ComputerpartsLibrary.MODEL;

namespace WebshopAPI.SERVICE
{
    public class OrderService : IOrderService
    {
        private readonly ComputerpatsDbContext _context;

        public OrderService(ComputerpatsDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Set<Order>().FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Set<Order>().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Set<Order>()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Order>()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var newOrder = new Order
            {
                OrderDate = order.OrderDate,
                UserId = order.UserId
            };

            _context.Set<Order>().Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Set<Order>().FindAsync(order.Id);
            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.UserId = order.UserId;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Set<Order>().FindAsync(id);
            if (order == null)
                return false;

            _context.Set<Order>().Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalOrderAmountAsync(int orderId)
        {
            var orderItems = await _context.Set<OrderItem>()
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();

            return orderItems.Sum(oi => oi.Quantity * (decimal)oi.Product.Price);
        }

        public async Task<int> GetTotalItemsCountAsync(int orderId)
        {
            var orderItems = await _context.Set<OrderItem>()
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();

            return orderItems.Sum(oi => oi.Quantity);
        }
    }
}
