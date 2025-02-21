namespace EasyCommerce.Models
{
    public class ProductCategory
    {
         public int Id { get; set; }  // Primary Key
        // Foreign key to Product
        public int ProductId { get; set; }
        public Product ? Product { get; set; }  // Navigation property to Product

        // Foreign key to Category
        public int CategoryId { get; set; }
        public Category ? Category { get; set; }  // Navigation property to Category
    }
}
