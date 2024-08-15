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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per night must be a non-negative value  and must greater then 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PricePerNight { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Availability must be a non-negative value  and must greater then 0.")]
        public int Availability { get; set; }

        [Required]
        [StringLength(17)]
        [Phone]
        [RegularExpression(@"^(\+1\s\d{3}\s\d{3}\s\d{4}|\(\d{3}\)\s\d{3}\s\d{4}|\d{3}\s\d{3}\s\d{4})$", ErrorMessage = "Phone number format is not valid. Accepted formats: +1 123 123 1234, (123) 123 1234, 123 123 1234.")]
        public string? Contact { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
