using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderVibe.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? FlightId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }

        [Required]
        [StringLength(100)]
        public string? Status { get; set; }

        public int? HotelId { get; set; }

        public TravelPackage TravelPackage { get; set; }
        public User User { get; set; }
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
    }
}
