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
        public decimal PricePerNight { get; set; }

        [Range(0, int.MaxValue)]
        public int Availability { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
