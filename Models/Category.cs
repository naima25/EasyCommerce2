using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Category
    {
        public int Id { get; set; }  
        public string? Name { get; set; }  

        // Many-to-many relationship: One Category can have many Products via ProductCategory
        [JsonIgnore]
        public List<ProductCategory>? ProductCategories { get; set; }  // This links to the ProductCategory table
    }
}
