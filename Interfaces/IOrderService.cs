using EasyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCommerce.DTO;

namespace EasyCommerce.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(int id, Order order);
        Task DeleteOrderAsync(int id);
    }
}
