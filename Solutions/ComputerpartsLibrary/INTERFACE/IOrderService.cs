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
        Task<Orders> GetOrderByIdAsync(int id);
        Task<IEnumerable<Orders>> GetAllOrdersAsync();
        Task AddOrderAsync(Orders order);
        Task UpdateOrderAsync(Orders order);
        Task DeleteOrderAsync(int id);
    }
}
