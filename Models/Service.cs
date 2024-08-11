using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderVibe.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        public string Destination { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        // Navigation property for the many-to-many relationship
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
