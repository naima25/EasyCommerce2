using EasyCommerce.Models;
using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Email { get; set; }

        // One-to-many relationship: A customer can have many orders
        [JsonIgnore]
        public List<Order> ? Orders { get; set; }
    }
}
