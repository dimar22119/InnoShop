using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Availability { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
