using System.ComponentModel.DataAnnotations;

namespace ProductManagementApi.Dtos
{
    public class CreateProductDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(25, ErrorMessage = "Name cannot be over 25 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
        [MaxLength(280, ErrorMessage = "Description cannot be over 280 characters")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Required]
        public string Availability { get; set; } = string.Empty;
    }
}
