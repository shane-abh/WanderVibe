namespace WanderVibe.Models
{
    public class UserBookingViewModel
    {
        public string? PackageName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? HotelName { get; set; }
        public string? HotelEmail { get; set; }
        public string? HotelPhoneNumber { get; set; }
        public string? FlightNumber { get; set; }
        public List<string>? Services { get; set; }
        public string? Status { get; set; }
        public string StatusBadgeClass
        {
            get
            {
                return Status switch
                {
                    "Completed" => "bg-success",
                    "Booked" => "bg-primary",
                    "On Going" => "bg-warning",
                    _ => "bg-secondary",
                };
            }
        }
    }
}
