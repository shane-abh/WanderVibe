﻿using System.ComponentModel.DataAnnotations;
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
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserProfile? User { get; set; }


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

        public TravelPackage? TravelPackage { get; set; }
        public Flight? Flight { get; set; }
        public Hotel? Hotel { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
