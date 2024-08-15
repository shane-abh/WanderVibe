using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderVibe.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        [Required]
        [StringLength(200)]
        public string? HotelName { get; set; }

        [Required]
        [StringLength(200)]
        public string? Location { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per night must be a non-negative value.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PricePerNight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Availability must be a non-negative value.")]
        public int Availability { get; set; }

        [Required]
        [StringLength(17)]
        [Phone]
        public string? Contact { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
