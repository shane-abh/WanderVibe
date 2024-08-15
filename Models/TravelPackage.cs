using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderVibe.Models
{
    public class TravelPackage
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string? PackageName { get; set; }

        [Required]        
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string? DestinationFrom { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string? DestinationTo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public int NoOfDays
        {
            get
            {
                return (EndDate - StartDate).Days;
            }
        }

        [Range(0, int.MaxValue, ErrorMessage = "Availability must be a non-negative number.")]
        public int Availability { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Booking>? Bookings { get; set; }
    }
}
