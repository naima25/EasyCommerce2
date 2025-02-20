using EasyCommerce.Models;
using EasyCommerce.Data;
using EasyCommerce.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly EasyCommerceContext _context;

        // Constructor to inject the DbContext
        public ProductService(EasyCommerceContext context)
        {
            _context = context;
        }

        // Get all products asynchronously
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Get a product by its ID asynchronously
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Add a new product asynchronously
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Update an existing product asynchronously
        public async Task UpdateProductAsync(int id, Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete a product asynchronously
        public async Task DeleteProductAsync(int id)
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
