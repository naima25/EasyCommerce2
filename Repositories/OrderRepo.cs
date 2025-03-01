using EasyCommerce.Models;
using EasyCommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Repositories
{
    public class OrderRepository
    {
        private readonly EasyCommerceContext _context;

        public OrderRepository(EasyCommerceContext context)
        {
            _context = context;
        }

         // Retrieves all Orders from the database asynchronously
        public async Task<IEnumerable<Order>> GetAllAsync() => await _context.Orders.ToListAsync();

         // Retrieves all Orders by ID from the database asynchronously
        public async Task<Order> GetByIdAsync(int id) => await _context.Orders.FindAsync(id);

         // Adds a Order to the database asynchronously
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

         // Updates an Order to the database asynchronously
        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

         // Deletes an Order by ID from the database asynchronously
        public async Task DeleteAsync(int id)
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
