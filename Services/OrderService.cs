using EasyCommerce.Models;
using EasyCommerce.Data;
using EasyCommerce.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EasyCommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly EasyCommerceContext _context;

        public OrderService(EasyCommerceContext context)
        {
            _context = context;
        }

        // Get all Orders asynchronously
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        // Get a Orders by its ID asynchronously
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        // Add a Order asynchronously
        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        //Update a Order asynchronously
        public async Task UpdateOrderAsync(int id, Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //Delete a Order asynchronously
        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
