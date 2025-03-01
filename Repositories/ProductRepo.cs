using EasyCommerce.Models;
using EasyCommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Repositories
{
    public class ProductRepository
    {
        private readonly EasyCommerceContext _context;

        public ProductRepository(EasyCommerceContext context)
        {
            _context = context;
        }

        // Retrieves all Products from the database asynchronously
        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        // Retrieves all Products by ID from the database asynchronously
        public async Task<Product> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        // Adds a Product to the database asynchronously
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Updates a product from the database asynchronously
        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Deletes a Product from the database asynchronously
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
