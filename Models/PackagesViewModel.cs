namespace WanderVibe.Models
{
    public class PackagesViewModel
    {
        public List<TravelPackage> Packages { get; set; }
        public List<string> UniqueDestinationsFrom { get; set; }
        public List<string> UniqueDestinationsTo { get; set; }
        public string? SelectedFrom { get; set; }
        public string? SelectedTo { get; set; } 
        public DateTime? SelectedDate { get; set; }  
    }
}
