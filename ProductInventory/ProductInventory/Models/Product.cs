using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInventory.Models
{
 
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;
    }
    
}
