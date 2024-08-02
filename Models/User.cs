using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WanderVibe.Models
{
    public class User : IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
