using System;
using System.Collections.Generic;

namespace EasyCommerce.Models
{
    public class Order
    {
        public int Id { get; set; }  
        public DateTime OrderDate { get; set; }  
        public int CustomerId { get; set; }  // Foreign key for Customer
        public Customer? Customer { get; set; }  

        public decimal TotalAmount { get; set; }  

        // One-to-many relationship: An order can have many order items
        public List<OrderItem>? OrderItems { get; set; }  // List of order items related to this order
    }
}

