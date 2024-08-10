using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WanderVibe.Models
{
    public class TravelDbContext : IdentityDbContext<UserProfile>
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options)
            : base(options)
        {
        }

        public DbSet<TravelPackage> TravelPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<TravelPackage>()
                .HasMany(tp => tp.Bookings)
                .WithOne(b => b.TravelPackage)
                .HasForeignKey(b => b.PackageId);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Bookings)
                .WithOne(b => b.Flight)
                .HasForeignKey(b => b.FlightId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Bookings)
                .WithOne(b => b.Hotel)
                .HasForeignKey(b => b.HotelId);
        }
    }
}
