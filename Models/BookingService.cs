using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WanderVibe.Models
{
    public class BookingService
    {
        [Key]
        public int BookingServiceId { get; set; }

        [Required]
        public int? BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Required]
        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}
