using EasyCommerce.Models;
using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Category
    {
        public int Id { get; set; }  
        public string ? Name { get; set; }  

        // One-to-many relationship: One Category can have many Products
        [JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}
