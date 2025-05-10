using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCommerce.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }  // Unique identifier for the cart

        [Required]
        public string CustomerId { get; set; }  // Foreign key for the customer

        // Navigation property for the related customer
        public Customer ? Customer { get; set; }  

        public List<CartItem> ? CartItems { get; set; } 

        public decimal Price { get; set; }  
    }
}
