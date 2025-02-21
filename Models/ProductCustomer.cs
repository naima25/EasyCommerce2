namespace EasyCommerce.Models
{
    public class ProductCustomer
    {
         public int Id { get; set; }  // Primary Key
        // Foreign key to Product
        public int ProductId { get; set; }
        public Product ? Product { get; set; }  // Navigation property to Product

        // Foreign key to Customer
        public int CustomerId { get; set; }
        public Customer ? Customer { get; set; }  // Navigation property to Customer

       
    }
}

