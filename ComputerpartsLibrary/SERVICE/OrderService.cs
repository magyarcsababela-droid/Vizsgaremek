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

        public async Task<orders> GetOrderByIdAsync(int id)
        {
            return await _context.Set<orders>().FindAsync(id);
        }

        public async Task<IEnumerable<orders>> GetAllOrdersAsync()
        {
            return await _context.Set<orders>().ToListAsync();
        }

        public async Task<IEnumerable<orders>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Set<orders>()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<orders>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<orders>()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<orders> CreateOrderAsync(orders order)
        {
            var newOrder = new orders
            {
                OrderDate = order.OrderDate,
                UserId = order.UserId
            };

            _context.Set<orders>().Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder;
        }

        public async Task<orders> UpdateOrderAsync(orders order)
        {
            var existingOrder = await _context.Set<orders>().FindAsync(order.Id);
            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.UserId = order.UserId;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Set<orders>().FindAsync(id);
            if (order == null)
                return false;

            _context.Set<orders>().Remove(order);
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
