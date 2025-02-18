using System;

namespace EasyCommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }  
        public int Quantity { get; set; }  
        
       
        public int OrderId { get; set; }  
        
        // Relationship: Many OrderItems can belong to one Product (Many-to-One)
        public int ProductId { get; set; }   // Foreign Key: This links the OrderItem to a specific Product
        
        public decimal Price { get; set; }  
    }
}

