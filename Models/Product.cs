using System.Text.Json.Serialization;
namespace EasyCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }  
        public string? Name { get; set; }  
        public decimal Price { get; set; }  

        // Relationship: Many Products can belong to one Category (Many-to-One)
        public int CategoryId { get; set; }  // Foreign Key to the Category

        // Navigation property: Allows accessing the Category associated with this Product
        [JsonIgnore]
        public Category? Category { get; set; }  
    }
}
