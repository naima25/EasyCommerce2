using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EasyCommerce.Models; 


namespace EasyCommerce.Models
{
    //Inherits from IdentityUser which adds authentication and identity features
    public class Customer : IdentityUser
    {
        public string? FullName { get; set; }  

        // One-to-many relationship: A customer can have multiple orders
        public List<Order>? Orders { get; set; }  

        // Navigation property to ProductCustomer table (Many-to-Many with Product)
        public List<ProductCustomer>? ProductCustomers { get; set; }  // Tracks products purchased by this customer
    }
}
