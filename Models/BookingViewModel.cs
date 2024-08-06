namespace WanderVibe.Models
{
    public class BookingViewModel
    {
        public UserProfile User { get; set; }
        public TravelPackage Package { get; set; }
        public IEnumerable<Hotel> Hotels { get; set; }
        public IEnumerable<Flight> Flights { get; set; }
        public int SelectedHotelId { get; set; }
        public int SelectedFlightId { get; set; }
        public int Quantity { get; set; }
        public Hotel SelectedHotel { get; set; }
        public Flight SelectedFlight { get; set; }
    }
}
