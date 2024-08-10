namespace WanderVibe.Models
{
    public class ServiceDetailViewModel
    {
        public TravelPackage Package { get; set; }
        public UserProfile User { get; set; }
        public List<Hotel>? Hotels { get; set; }
        public List<Flight>? Flights { get; set; }
        public List<Service>? Services { get; set; }
        public int? SelectedHotelId { get; set; }
        public Hotel? SelectedHotel { get; set; }
        public int? SelectedFlightId { get; set; }
        public Flight? SelectedFlight { get; set; }
        public List<int> SelectedServiceIds { get; set; } = new List<int>();
        public int? Quantity { get; set; }
    }
}
