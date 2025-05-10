using EasyCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace EasyCommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }  
        public int Quantity { get; set; }  
        
        public int ProductId { get; set; }  
        public Product ? Product { get; set; } 
    }
}

