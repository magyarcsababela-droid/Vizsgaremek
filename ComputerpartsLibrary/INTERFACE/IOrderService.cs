using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IOrderService
    {
        Task<orders> GetOrderByIdAsync(int id);
        Task<IEnumerable<orders>> GetAllOrdersAsync();
        Task<IEnumerable<orders>> GetOrdersByUserIdAsync(int userId);
        Task<orders> CreateOrderAsync(orders order);
        Task<orders> UpdateOrderAsync(orders order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
