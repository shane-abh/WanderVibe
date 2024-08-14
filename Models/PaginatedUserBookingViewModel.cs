namespace WanderVibe.Models
{
    public class PaginatedUserBookingViewModel
    {
        public IEnumerable<UserBookingViewModel>? Bookings { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
