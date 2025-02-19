using Microsoft.EntityFrameworkCore;
using EasyCommerce.Models; 


namespace EasyCommerce.Data 
{
    public class EasyCommerceContext : DbContext
    {
        public EasyCommerceContext(DbContextOptions<EasyCommerceContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
