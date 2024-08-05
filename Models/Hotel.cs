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
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PricePerNight { get; set; }

        [Range(0, int.MaxValue)]
        public int Availability { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "The phone number must be in the format 123-456-7890.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "The email address must be in the format someone@example.com.")]
        public string EmailAddress { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
