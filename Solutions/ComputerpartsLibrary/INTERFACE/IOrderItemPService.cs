using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IOrderItemPService
    {
        Task<Order_items_p> GetOrderItemPByIdAsync(int itemId);
        Task<IEnumerable<Order_items_p>> GetAllOrderItemPsAsync();
        Task AddOrderItemPAsync(Order_items_p orderItemP);
        Task UpdateOrderItemPAsync(Order_items_p orderItemP);
        Task DeleteOrderItemPAsync(int itemId);
    }
}
