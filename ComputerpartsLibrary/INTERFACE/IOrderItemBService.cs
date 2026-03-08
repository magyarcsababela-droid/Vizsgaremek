using ComputerpartsLibrary.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.INTERFACE
{
    public interface IOrderItemBService
    {
        Task<Order_items_b> GetOrderItemBByIdAsync(int itemId);
        Task<IEnumerable<Order_items_b>> GetAllOrderItemBsAsync();
        Task AddOrderItemBAsync(Order_items_b orderItemB);
        Task UpdateOrderItemBAsync(Order_items_b orderItemB);
        Task DeleteOrderItemBAsync(int itemId);
    }
}
