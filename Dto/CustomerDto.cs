using System.ComponentModel.DataAnnotations;

namespace EasyCommerce.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string ? Name { get; set; }

        [Required]
        [EmailAddress]
        public string ? Email { get; set; }

    }
}
