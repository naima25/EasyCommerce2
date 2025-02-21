using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyCommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }

        // One-to-many relationship: An order can have multiple order items
        [JsonIgnore]
        public List<OrderItem>? OrderItems { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }  // This navigation property automatically refers to the CustomerId
    }
}
