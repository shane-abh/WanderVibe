using System;
using System.Collections.Generic;

namespace WanderVibe.Models
{
    public class BookingSummaryViewModel
    {
        public int BookingId { get; set; }
        public TravelPackage Package { get; set; }
        public Hotel SelectedHotel { get; set; }
        public Flight SelectedFlight { get; set; }
        public List<Service> SelectedServices { get; set; } = new List<Service>();
        public int Quantity { get; set; }

        public decimal? TotalCost
        {
            get
            {
                decimal? total = 0;

                // Calculate the total cost based on the selected package, hotel, flight, and services
                if (Package != null)
                    total += Package.Price * Quantity;

                if (SelectedHotel != null)
                {
                    int numberOfRooms = (int)Math.Ceiling(Quantity / 2.0);
                    total += SelectedHotel.PricePerNight * numberOfRooms;
                }

                if (SelectedFlight != null)
                    total += SelectedFlight.Price * Quantity;

                if (SelectedServices != null)
                {
                    foreach (var service in SelectedServices)
                    {
                        total += service.Price * Quantity;
                    }
                }

                return total;
            }
        }
    }
}

