using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EasyCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace EasyCommerce.Data 
{
    public class EasyCommerceContext : IdentityDbContext<IdentityUser>
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

