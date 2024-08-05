using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WanderVibe.Models
{
    public class UserProfile : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(50)]
        public string? PreferredLanguage { get; set; }

        [StringLength(200)]
        public string? EmergencyContacts { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
