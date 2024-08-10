using Microsoft.EntityFrameworkCore;
using WanderVibe.Models;
using System;

namespace WanderVibe.Data
{
    public static class TravelDbContextSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelPackage>().HasData(
                new TravelPackage { PackageId = 1, PackageName = "Toronto to Vancouver Adventure", Description = "Explore the beautiful city of Vancouver with this all-inclusive package.", Price = 1200.00m, DestinationFrom = "Toronto", DestinationTo = "Vancouver", StartDate = new DateTime(2024, 9, 1), EndDate = new DateTime(2024, 9, 7), Availability = 50, ImageUrl = "Vancouver Adventure.jpg" },
                new TravelPackage { PackageId = 2, PackageName = "Calgary to Montreal Cultural Tour", Description = "Experience the vibrant culture of Montreal with guided tours and more.", Price = 1100.00m, DestinationFrom = "Calgary", DestinationTo = "Montreal", StartDate = new DateTime(2024, 9, 10), EndDate = new DateTime(2024, 9, 15), Availability = 40, ImageUrl = "Montreal Cultural.webp" },
                new TravelPackage { PackageId = 3, PackageName = "Vancouver to Banff Nature Escape", Description = "Enjoy the scenic beauty of Banff with this relaxing getaway.", Price = 1000.00m, DestinationFrom = "Vancouver", DestinationTo = "Banff", StartDate = new DateTime(2024, 9, 20), EndDate = new DateTime(2024, 9, 25), Availability = 45, ImageUrl = "Banff Nature.webp" },
                new TravelPackage { PackageId = 4, PackageName = "Montreal to Toronto Urban Experience", Description = "Discover the excitement of Toronto in this urban adventure.", Price = 1300.00m, DestinationFrom = "Montreal", DestinationTo = "Toronto", StartDate = new DateTime(2024, 9, 30), EndDate = new DateTime(2024, 10, 5), Availability = 55, ImageUrl = "Toronto Urban.jpg" },
                new TravelPackage { PackageId = 5, PackageName = "Toronto to Calgary Scenic Journey", Description = "A scenic journey from Toronto to Calgary with breathtaking views.", Price = 1400.00m, DestinationFrom = "Toronto", DestinationTo = "Calgary", StartDate = new DateTime(2024, 10, 10), EndDate = new DateTime(2024, 10, 15), Availability = 60, ImageUrl = "Calgary Scenic.jpg" },
                new TravelPackage { PackageId = 6, PackageName = "Vancouver to Montreal Explorer", Description = "A comprehensive tour from Vancouver to Montreal, covering the best sights.", Price = 1500.00m, DestinationFrom = "Vancouver", DestinationTo = "Montreal", StartDate = new DateTime(2024, 10, 20), EndDate = new DateTime(2024, 10, 27), Availability = 65, ImageUrl = "Montreal Explorer.webp" },
                new TravelPackage { PackageId = 7, PackageName = "Calgary to Banff Mountain Retreat", Description = "A retreat into the mountains from Calgary to Banff, perfect for nature lovers.", Price = 900.00m, DestinationFrom = "Calgary", DestinationTo = "Banff", StartDate = new DateTime(2024, 11, 1), EndDate = new DateTime(2024, 11, 5), Availability = 70, ImageUrl = "Banff Retreat.jpg" }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { HotelId = 1, HotelName = "The Grand Hotel", Location = "Toronto", PricePerNight = 150.00m, Availability = 20, Contact = "416-123-4567", Email = "contact@grandhotel.com" },
                new Hotel { HotelId = 2, HotelName = "Ocean View Resort", Location = "Vancouver", PricePerNight = 200.00m, Availability = 15, Contact = "604-789-0123", Email = "info@oceanview.com" },
                new Hotel { HotelId = 3, HotelName = "Mountain Retreat", Location = "Banff", PricePerNight = 175.00m, Availability = 10, Contact = "403-456-7890", Email = "stay@mountainretreat.com" },
                new Hotel { HotelId = 4, HotelName = "City Lights Hotel", Location = "Montreal", PricePerNight = 180.00m, Availability = 25, Contact = "514-654-3210", Email = "booking@citylightshotel.com" },
                new Hotel { HotelId = 5, HotelName = "Sunset Inn", Location = "Calgary", PricePerNight = 130.00m, Availability = 30, Contact = "403-234-5678", Email = "reservations@sunsetinn.com" },
                new Hotel { HotelId = 6, HotelName = "Bayfront Hotel", Location = "Toronto", PricePerNight = 160.00m, Availability = 20, Contact = "416-222-3333", Email = "contact@bayfront.com" },
                new Hotel { HotelId = 7, HotelName = "Lakeside Inn", Location = "Toronto", PricePerNight = 140.00m, Availability = 18, Contact = "416-444-5555", Email = "info@lakesideinn.com" },
                new Hotel { HotelId = 8, HotelName = "Rocky Mountain Lodge", Location = "Banff", PricePerNight = 185.00m, Availability = 12, Contact = "403-333-4444", Email = "stay@rockymountainlodge.com" },
                new Hotel { HotelId = 9, HotelName = "Downtown Hotel", Location = "Montreal", PricePerNight = 190.00m, Availability = 22, Contact = "514-777-8888", Email = "booking@downtownhotel.com" },
                new Hotel { HotelId = 10, HotelName = "Seaside Resort", Location = "Vancouver", PricePerNight = 210.00m, Availability = 15, Contact = "604-555-6666", Email = "info@seasideresort.com" },
                new Hotel { HotelId = 11, HotelName = "Maple Leaf Hotel", Location = "Montreal", PricePerNight = 170.00m, Availability = 20, Contact = "514-888-9999", Email = "reservations@mapleleafhotel.com" },
                new Hotel { HotelId = 12, HotelName = "Skyline Hotel", Location = "Calgary", PricePerNight = 135.00m, Availability = 28, Contact = "403-999-1111", Email = "reservations@skylinehotel.com" },
                new Hotel { HotelId = 13, HotelName = "Urban Stay", Location = "Toronto", PricePerNight = 155.00m, Availability = 25, Contact = "416-567-7890", Email = "info@urbanstay.com" },
                new Hotel { HotelId = 14, HotelName = "Coastal Breeze Inn", Location = "Vancouver", PricePerNight = 205.00m, Availability = 18, Contact = "604-123-4567", Email = "contact@coastalbreeze.com" },
                new Hotel { HotelId = 15, HotelName = "Alpine Resort", Location = "Banff", PricePerNight = 195.00m, Availability = 15, Contact = "403-789-0123", Email = "reservations@alpineresort.com" },
                new Hotel { HotelId = 16, HotelName = "Downtown Lodge", Location = "Montreal", PricePerNight = 185.00m, Availability = 20, Contact = "514-567-1234", Email = "info@downtownlodge.com" }
            );

            modelBuilder.Entity<Flight>().HasData(
                new Flight { FlightId = 1, FlightNumber = "AC101", DepartureCity = "Toronto", ArrivalCity = "Vancouver", DepartureDate = new DateTime(2024, 8, 10, 8, 0, 0), ArrivalDate = new DateTime(2024, 8, 10, 11, 0, 0), Price = 500.00m, Availability = 150 },
                new Flight { FlightId = 2, FlightNumber = "WS202", DepartureCity = "Calgary", ArrivalCity = "Montreal", DepartureDate = new DateTime(2024, 8, 12, 9, 0, 0), ArrivalDate = new DateTime(2024, 8, 12, 15, 0, 0), Price = 450.00m, Availability = 120 },
                new Flight { FlightId = 3, FlightNumber = "DL303", DepartureCity = "Vancouver", ArrivalCity = "Toronto", DepartureDate = new DateTime(2024, 8, 15, 10, 0, 0), ArrivalDate = new DateTime(2024, 8, 15, 16, 0, 0), Price = 550.00m, Availability = 100 },
                new Flight { FlightId = 4, FlightNumber = "UA404", DepartureCity = "Montreal", ArrivalCity = "Calgary", DepartureDate = new DateTime(2024, 8, 18, 7, 0, 0), ArrivalDate = new DateTime(2024, 8, 18, 12, 0, 0), Price = 470.00m, Availability = 130 },
                new Flight { FlightId = 5, FlightNumber = "AC203", DepartureCity = "Toronto", ArrivalCity = "Montreal", DepartureDate = new DateTime(2024, 9, 5, 14, 0, 0), ArrivalDate = new DateTime(2024, 9, 5, 16, 0, 0), Price = 350.00m, Availability = 160 },
                new Flight { FlightId = 6, FlightNumber = "WS304", DepartureCity = "Vancouver", ArrivalCity = "Calgary", DepartureDate = new DateTime(2024, 9, 7, 12, 0, 0), ArrivalDate = new DateTime(2024, 9, 7, 14, 0, 0), Price = 380.00m, Availability = 140 },
                new Flight { FlightId = 7, FlightNumber = "DL405", DepartureCity = "Toronto", ArrivalCity = "Banff", DepartureDate = new DateTime(2024, 9, 10, 9, 0, 0), ArrivalDate = new DateTime(2024, 9, 10, 12, 0, 0), Price = 520.00m, Availability = 130 },
                new Flight { FlightId = 8, FlightNumber = "UA506", DepartureCity = "Montreal", ArrivalCity = "Vancouver", DepartureDate = new DateTime(2024, 9, 15, 13, 0, 0), ArrivalDate = new DateTime(2024, 9, 15, 17, 0, 0), Price = 580.00m, Availability = 110 },
                new Flight { FlightId = 9, FlightNumber = "AC607", DepartureCity = "Calgary", ArrivalCity = "Toronto", DepartureDate = new DateTime(2024, 9, 18, 11, 0, 0), ArrivalDate = new DateTime(2024, 9, 18, 15, 0, 0), Price = 400.00m, Availability = 150 },
                new Flight { FlightId = 10, FlightNumber = "WS708", DepartureCity = "Vancouver", ArrivalCity = "Banff", DepartureDate = new DateTime(2024, 9, 20, 10, 0, 0), ArrivalDate = new DateTime(2024, 9, 20, 12, 0, 0), Price = 300.00m, Availability = 100 },
                new Flight { FlightId = 11, FlightNumber = "DL809", DepartureCity = "Toronto", ArrivalCity = "Vancouver", DepartureDate = new DateTime(2024, 9, 25, 16, 0, 0), ArrivalDate = new DateTime(2024, 9, 25, 19, 0, 0), Price = 490.00m, Availability = 120 },
                new Flight { FlightId = 12, FlightNumber = "UA910", DepartureCity = "Montreal", ArrivalCity = "Calgary", DepartureDate = new DateTime(2024, 9, 30, 8, 0, 0), ArrivalDate = new DateTime(2024, 9, 30, 12, 0, 0), Price = 450.00m, Availability = 140 },
                new Flight { FlightId = 13, FlightNumber = "AC304", DepartureCity = "Toronto", ArrivalCity = "Vancouver", DepartureDate = new DateTime(2024, 10, 10, 9, 0, 0), ArrivalDate = new DateTime(2024, 10, 10, 12, 0, 0), Price = 520.00m, Availability = 140 },
                new Flight { FlightId = 14, FlightNumber = "WS405", DepartureCity = "Calgary", ArrivalCity = "Montreal", DepartureDate = new DateTime(2024, 10, 12, 10, 0, 0), ArrivalDate = new DateTime(2024, 10, 12, 15, 0, 0), Price = 480.00m, Availability = 130 },
                new Flight { FlightId = 15, FlightNumber = "DL506", DepartureCity = "Vancouver", ArrivalCity = "Toronto", DepartureDate = new DateTime(2024, 10, 15, 11, 0, 0), ArrivalDate = new DateTime(2024, 10, 15, 17, 0, 0), Price = 550.00m, Availability = 110 },
                new Flight { FlightId = 16, FlightNumber = "UA607", DepartureCity = "Montreal", ArrivalCity = "Calgary", DepartureDate = new DateTime(2024, 10, 18, 7, 0, 0), ArrivalDate = new DateTime(2024, 10, 18, 12, 0, 0), Price = 460.00m, Availability = 120 }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, Name = "Swimming", Price = 50.00m, Destination = "Vancouver" },
                new Service { ServiceId = 2, Name = "Guided City Tour", Price = 100.00m, Destination = "Montreal" },
                new Service { ServiceId = 3, Name = "Spa Package", Price = 200.00m, Destination = "Banff" },
                new Service { ServiceId = 4, Name = "Museum Entry", Price = 30.00m, Destination = "Toronto" },
                new Service { ServiceId = 5, Name = "Dinner Cruise", Price = 150.00m, Destination = "Toronto" },
                new Service { ServiceId = 6, Name = "Hiking Adventure", Price = 75.00m, Destination = "Banff" },
                new Service { ServiceId = 7, Name = "Whale Watching Tour", Price = 120.00m, Destination = "Vancouver" },
                new Service { ServiceId = 8, Name = "City Lights Night Tour", Price = 90.00m, Destination = "Montreal" },
                new Service { ServiceId = 9, Name = "Winery Tour", Price = 80.00m, Destination = "Montreal" },
                new Service { ServiceId = 10, Name = "Rock Climbing", Price = 180.00m, Destination = "Banff" },
                new Service { ServiceId = 11, Name = "Kayaking Experience", Price = 100.00m, Destination = "Vancouver" },
                new Service { ServiceId = 12, Name = "Helicopter Ride", Price = 300.00m, Destination = "Banff" },
                new Service { ServiceId = 13, Name = "Skiing", Price = 150.00m, Destination = "Banff" },
                new Service { ServiceId = 14, Name = "Wine Tasting", Price = 110.00m, Destination = "Montreal" },
                new Service { ServiceId = 15, Name = "Fishing Trip", Price = 130.00m, Destination = "Vancouver" },
                new Service { ServiceId = 16, Name = "Art Gallery Entry", Price = 50.00m, Destination = "Toronto" }
            );
        }
    }
}
