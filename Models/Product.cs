using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }  

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ? Name { get; set; }  

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }  

        [Required]
        public int CategoryId { get; set; }  

        [JsonIgnore]
        public Category? Category { get; set; }  
    }
}
