using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Customer
    {
        public int Id { get; set; }  

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }  

        [Required]
        [EmailAddress]
        public string? Email { get; set; }  

        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string? PhoneNumber { get; set; }  

        // One-to-many relationship: A customer can have multiple orders
        [JsonIgnore]
        public List<Order>? Orders { get; set; }  

        // Navigation property to ProductCustomer table (Many-to-Many with Product)
        public List<ProductCustomer>? ProductCustomers { get; set; }  // Tracks products purchased by this customer
    }
}
