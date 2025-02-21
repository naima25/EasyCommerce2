using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EasyCommerce.Models;

namespace EasyCommerce.Data
{
    public class EasyCommerceContext : IdentityDbContext<Customer>  // Use Customer instead of IdentityUser
    {
        public EasyCommerceContext(DbContextOptions<EasyCommerceContext> options) : base(options) {}

        // DbSets for other models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCustomer> ProductCustomers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
