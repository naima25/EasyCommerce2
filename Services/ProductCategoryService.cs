using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCommerce.Data;
using EasyCommerce.Interfaces;
using EasyCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCommerce.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly EasyCommerceContext _context;

        public ProductCategoryService(EasyCommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context
                .ProductCategories.Include(pc => pc.Product) // Fetch product details (including the name)
                .Include(pc => pc.Category) // Fetch category details (including the name)
                .ToListAsync();
        }

        public async Task AddAsync(ProductCategory pc)
        {
            _context.ProductCategories.Add(pc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, int categoryId)
        {
            var pc = await _context.ProductCategories.FindAsync(productId, categoryId);
            if (pc != null)
            {
                _context.ProductCategories.Remove(pc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
