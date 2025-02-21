using System.ComponentModel.DataAnnotations;

namespace EasyCommerce.DTO
{
    public class CustomerDTO
    {
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string? FullName { get; set; }

        // Navigation property to ProductCustomer table (many-to-many relationship with Product)
        public List<int>? ProductCustomerIds { get; set; } // List of related ProductCustomer IDs

        // Relationship: Orders (List of Order IDs only)
        public List<int>? OrderIds { get; set; } // List of related Order IDs
    }
}
