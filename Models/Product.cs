using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EasyCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }  

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }  

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }  

        // Navigation property to ProductCategory table (Many-to-Many)
        public List<ProductCategory>? ProductCategories { get; set; }  

        // Navigation property to ProductCustomer table (Many-to-Many with Customer)
        public List<ProductCustomer>? ProductCustomers { get; set; }  // Tracks customers who purchased this product
    }
}
