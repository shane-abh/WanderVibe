using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Booking
{
    [Authorize(Roles = "Client")]
    public class BookingController : Controller
    {
        private readonly TravelDbContext _context;
        private readonly UserManager<UserProfile> _userManager;

        public BookingController(TravelDbContext context, UserManager<UserProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> BookNow(int id, int? selectedHotelId, int? selectedFlightId, int? quantity)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var hotels = _context.Hotels.Where(h => h.Location == package.DestinationTo).ToList();
            var flights = _context.Flights.Where(f => f.ArrivalCity == package.DestinationTo && f.DepartureCity == package.DestinationFrom).ToList();
            int hotelId = 0;
            Hotel hotel = null;
            if (selectedHotelId != null)
            {
                hotelId = selectedHotelId.Value;
                hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == hotelId);
            }
            int flightId = 0;
            Flight flight = null;
            if (selectedFlightId != null)
            {
                flightId = selectedFlightId.Value;
                flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);
            }
            int qty = 0;
            if (quantity != null)
            {
                qty = quantity.Value;
            }
            var model = new BookingViewModel
            {
                User = user,
                Package = package,
                Hotels = hotels,
                Flights = flights,
                SelectedHotelId = hotelId,
                SelectedHotel = hotel,
                SelectedFlight = flight,
                SelectedFlightId = flightId,
                Quantity = qty,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult BookNow(BookingViewModel model)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == model.Package.PackageId);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == model.SelectedHotelId);
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == model.SelectedFlightId);

            var totalPackagePrice = model.Quantity * package?.Price;
            var numberOfRooms = (int)Math.Ceiling((double)model.Quantity / 2);
            var totalHotelPrice = hotel != null ? numberOfRooms * hotel.PricePerNight : 0;
            var totalFlightPrice = flight != null ? model.Quantity * flight.Price : 0;
            var totalCost = totalPackagePrice + totalHotelPrice + totalFlightPrice;

            var booking = new Models.Booking
            {
                PackageId = package.PackageId,
                UserId = _userManager.GetUserId(User),
                FlightId = flight.FlightId,
                BookingDate = DateTime.Now,
                TotalCost = totalCost,
                Status = "Confirmed",
                HotelId = hotel.HotelId
            };

            _context.Bookings.Add(booking);

            package.Availability -= model.Quantity;
            hotel.Availability -= numberOfRooms;
            flight.Availability -= model.Quantity;

            _context.SaveChanges();

            TempData["Success"] = "Booking Done Successfully";

            return RedirectToAction("Packages", "Home");
        }

        [HttpPost]
        public IActionResult UpdateHotel(BookingViewModel model)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == model.Package.PackageId);
            if (package == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var hotels = _context.Hotels.Where(h => h.Location == package.DestinationTo).ToList();
            var flights = _context.Flights.Where(f => f.ArrivalCity == package.DestinationTo && f.DepartureCity == package.DestinationFrom).ToList();

            model.User = user;
            model.Package = package;
            model.Hotels = hotels;
            model.Flights = flights;
            model.SelectedHotel = _context.Hotels.FirstOrDefault(h => h.HotelId == model.SelectedHotelId);
            model.SelectedFlight = _context.Flights.FirstOrDefault(f => f.FlightId == model.SelectedFlightId);

            return View("BookNow", model);
        }

        [HttpPost]
        public IActionResult UpdateFlight(BookingViewModel model)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == model.Package.PackageId);
            if (package == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var hotels = _context.Hotels.Where(h => h.Location == package.DestinationTo).ToList();
            var flights = _context.Flights.Where(f => f.ArrivalCity == package.DestinationTo && f.DepartureCity == package.DestinationFrom).ToList();


            model.User = user;
            model.Package = package;
            model.Hotels = hotels;
            model.Flights = flights;
            model.SelectedHotel = _context.Hotels.FirstOrDefault(h => h.HotelId == model.SelectedHotelId);
            model.SelectedFlight = _context.Flights.FirstOrDefault(f => f.FlightId == model.SelectedFlightId);


            return View("BookNow", model);
        }

        [HttpPost]
        public IActionResult CalculatePrice(BookingViewModel model)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == model.Package.PackageId);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == model.SelectedHotelId);
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == model.SelectedFlightId);
            int? quantity = model.Quantity;

            if (hotel == null)
            {
                TempData["Error"] = "Select hotel First.";
                return RedirectToAction("BookNow", "Booking", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }
            if (flight == null)
            {
                TempData["Error"] = "Select Flight First.";
                return RedirectToAction("BookNow", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }
            if (quantity == 0)
            {
                TempData["Error"] = "Select Person Quantity.";
                return RedirectToAction("BookNow", "Booking", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }
            if (package?.Availability < model.Quantity)
            {
                TempData["Error"] = "Not enough availability for the selected package.";
                return RedirectToAction("BookNow", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }

            if (hotel.Availability < model.Quantity)
            {
                TempData["Error"] = "Not enough availability for the selected hotel.";
                return RedirectToAction("BookNow", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }

            if (flight.Availability < model.Quantity)
            {
                TempData["Error"] = "Not enough availability for the selected flight.";
                return RedirectToAction("BookNow", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId, selectedFlightId = model.SelectedFlightId, quantity = model.Quantity });
            }

            var totalPackagePrice = model.Quantity * package?.Price;
            var numberOfRooms = (int)Math.Ceiling((double)model.Quantity / 2);
            var totalHotelPrice = hotel != null ? numberOfRooms * hotel.PricePerNight : 0;
            var totalFlightPrice = flight != null ? model.Quantity * flight.Price : 0;
            var totalCost = totalPackagePrice + totalHotelPrice + totalFlightPrice;

            TempData["TotalPrice"] = totalCost.ToString();
            TempData["TotalPackagePrice"] = totalPackagePrice.ToString();
            TempData["TotalHotelPrice"] = totalHotelPrice.ToString();
            TempData["TotalFlightPrice"] = totalFlightPrice.ToString();
            TempData["NumberOfRooms"] = numberOfRooms.ToString();

            return RedirectToAction("BookNow", new { id = model.Package.PackageId, selectedHotelId = model.SelectedHotelId , selectedFlightId= model.SelectedFlightId , quantity = model.Quantity });
        }
    }
}
