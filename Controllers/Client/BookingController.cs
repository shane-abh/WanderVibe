using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Client
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
        public async Task<IActionResult> PackageDetails(int id)
        {
            var package = await _context.TravelPackages.FirstOrDefaultAsync(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var model = new BasicDetailViewModel
            {
                User = user,
                Package = package
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PackageDetails(BasicDetailViewModel model)
        {
            // Create a new Booking object with the data from the form
            var booking = new Booking
            {
                PackageId = model.Package.PackageId,
                UserId = _userManager.GetUserId(User),
                BookingDate = DateTime.Now,
                Status = "Pending"
            };

            // Save the booking to the database
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Store additional data in TempData
            TempData["DestinationFrom"] = model.Package.DestinationFrom;
            TempData["DestinationTo"] = model.Package.DestinationTo;
            TempData["PackageName"] = model.Package.PackageName;

            // Pass the necessary data to the next page, such as the BookingId
            return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
        }

        [HttpGet]
        public IActionResult BackToHome(int packageId)
        {
            var userId = _userManager.GetUserId(User);

            // Find the pending booking for the current user and package
            var pendingBooking = _context.Bookings
                                         .FirstOrDefault(b => b.PackageId == packageId && b.UserId == userId && b.Status == "Pending");

            // If a pending booking exists, remove it
            if (pendingBooking != null)
            {
                _context.Bookings.Remove(pendingBooking);
                _context.SaveChanges();
            }

            // Redirect to the Home/Index action
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ServicesDetails(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Ensure that the TravelPackage is properly loaded
            if (booking.TravelPackage == null)
            {
                return NotFound("Travel package not found for this booking.");
            }

            // Fetch unique hotels based on the destination of the travel package
            var hotels = await _context.Hotels
                .Where(h => h.Location == booking.TravelPackage.DestinationTo)
                .GroupBy(h => h.HotelName) 
                .Select(g => g.First())
                .ToListAsync();

            // Fetch unique flights based on the travel package's destination and departure locations
            var flights = await _context.Flights
                .Where(f => f.ArrivalCity == booking.TravelPackage.DestinationTo && f.DepartureCity == booking.TravelPackage.DestinationFrom)
                .GroupBy(f => f.FlightNumber)  
                .Select(g => g.First())
                .ToListAsync();

            // Prepare the view model with necessary data
            var model = new ServiceDetailViewModel
            {
                User = booking.User,
                Package = booking.TravelPackage,
                Hotels = hotels,
                Flights = flights,
                Quantity = null
            };
            ViewBag.BookingId = bookingId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ServicesDetails(ServiceDetailViewModel model, string action, int bookingId)
        {
            // Get the current user's ID
            var userId = _userManager.GetUserId(User);

            // Find the booking based on BookingId, UserId, and Status == "Pending"
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId
                                        && b.UserId == userId
                                        && b.Status == "Pending");

            if (booking == null)
            {
                return NotFound();
            }

            // Fetch all hotels and flights for the dropdowns
            var allHotels = await _context.Hotels
                .Where(h => h.Location == booking.TravelPackage.DestinationTo)
                .ToListAsync();

            var allFlights = await _context.Flights
                .Where(f => f.ArrivalCity == booking.TravelPackage.DestinationTo
                            && f.DepartureCity == booking.TravelPackage.DestinationFrom)
                .ToListAsync();

            model.Hotels = allHotels
                .DistinctBy(h => h.HotelName)
                .ToList();

            model.Flights = allFlights
                .DistinctBy(f => f.FlightNumber)
                .ToList();


            // Handle the "Reset" action
            if (action == "Reset")
            {
                model.SelectedHotelId = null;
                model.SelectedHotel = null;
                model.SelectedFlightId = null;
                model.SelectedFlight = null;
                model.Quantity = null;

                // Redirect back to the same action with the bookingId
                return RedirectToAction("ServicesDetails", "Booking" ,new { bookingId = bookingId });
            }

            // Handle the "Get Details" action for Hotel
            if (action == "GetHotelDetails")
            {
                model.SelectedHotel = await _context.Hotels
                    .FirstOrDefaultAsync(h => h.HotelId == model.SelectedHotelId);

                model.SelectedHotelId = model.SelectedHotel?.HotelId;

                // Ensure the flight selection persists if a flight has been selected
                if (model.SelectedFlightId.HasValue)
                {
                    model.SelectedFlight = await _context.Flights
                        .FirstOrDefaultAsync(f => f.FlightId == model.SelectedFlightId);
                }
            }

            // Handle the "Get Details" action for Flight
            if (action == "GetFlightDetails")
            {
                model.SelectedFlight = await _context.Flights
                    .FirstOrDefaultAsync(f => f.FlightId == model.SelectedFlightId);

                model.SelectedFlightId = model.SelectedFlight?.FlightId;

                // Ensure the hotel selection persists if a hotel has been selected
                if (model.SelectedHotelId.HasValue)
                {
                    model.SelectedHotel = await _context.Hotels
                        .FirstOrDefaultAsync(h => h.HotelId == model.SelectedHotelId);
                }
            }

            // Handle the "Next" action
            if (action == "Next")
            {
                // Check if a hotel is selected
                if (model.SelectedHotelId == null )
                {
                    TempData["Error"] = "Please Select a Hotel First";
                    return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
                }

                // Check if a flight is selected
                if (model.SelectedFlightId == null)
                {
                    TempData["Error"] = "Please Select a Flight First. ";
                    return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
                }

                if (model.Quantity == null || model.Quantity <= 0)
                {
                    TempData["Error"] = "Quantity must be Greater then Zero.";
                    return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
                }

                // Fetch the selected hotel and flight details
                var selectedHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == model.SelectedHotelId);
                var selectedFlight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == model.SelectedFlightId);

                // Check hotel room availability (1 room per 2 people)
                int requiredRooms = (int)Math.Ceiling(model.Quantity.Value / 2.0);
                if (selectedHotel != null && selectedHotel.Availability < requiredRooms)
                {
                    TempData["Error"] = $"Not enough rooms available in {selectedHotel.HotelName}. Only {selectedHotel.Availability} rooms are available.";
                    return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
                }

                // Check flight availability
                if (selectedFlight != null && selectedFlight.Availability < model.Quantity)
                {
                    TempData["Error"] = $"Not enough seats available on flight {selectedFlight.FlightNumber}. Only {selectedFlight.Availability} seats are available.";
                    return RedirectToAction("ServicesDetails", "Booking", new { bookingId = booking.BookingId });
                }

                // Update the booking with selected hotel, flight, and quantity
                booking.HotelId = model.SelectedHotelId;
                booking.FlightId = model.SelectedFlightId;

                // Store the updated booking in the database
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                TempData["Quantity"] = model.Quantity;
                // Redirect to the OrderSummary page, passing the bookingId
                return RedirectToAction("ExtraServices", "Booking", new { bookingId = booking.BookingId });
            }

            ViewBag.BookingId = bookingId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExtraServices(int bookingId)
        {
            // Find the booking based on BookingId
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Ensure that the TravelPackage is properly loaded
            if (booking.TravelPackage == null)
            {
                return NotFound("Travel package not found for this booking.");
            }

            // Fetch services based on the travel package destination
            var services = await _context.Services
                .Where(s => s.Destination == booking.TravelPackage.DestinationTo)
                .ToListAsync();

            // Prepare the view model with necessary data
            var model = new ServiceDetailViewModel
            {
                User = booking.User,
                Package = booking.TravelPackage,
                Services = services, 
                SelectedHotelId = booking.HotelId,
                SelectedFlightId = booking.FlightId,
            };

            ViewBag.BookingId = bookingId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExtraServices(ServiceDetailViewModel model, int bookingId)
        {
            // Get the current user's ID
            var userId = _userManager.GetUserId(User);

            // Find the booking based on BookingId, UserId, and Status == "Pending"
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId
                                        && b.UserId == userId
                                        && b.Status == "Pending");

            if (booking == null)
            {
                return NotFound();
            }

            // Check if any services were selected
            if (model.SelectedServiceIds != null && model.SelectedServiceIds.Any())
            {
                // Add the selected services to the booking
                foreach (var serviceId in model.SelectedServiceIds)
                {
                    var bookingService = new BookingService
                    {
                        BookingId = booking.BookingId,
                        ServiceId = serviceId
                    };
                    _context.BookingServices.Add(bookingService);
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }

            // Redirect to the OrderSummary page, passing the bookingId
            return RedirectToAction("BookingSummary", "Booking", new { bookingId = booking.BookingId });
            }

        [HttpGet]
        public async Task<IActionResult> BookingSummary(int bookingId)
        {
            // Retrieve the booking with the related entities
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Retrieve the quantity from TempData
            int quantity = TempData["Quantity"] != null ? Convert.ToInt32(TempData["Quantity"]) : 1;

            // Prepare the view model
            var model = new BookingSummaryViewModel
            {
                BookingId = booking.BookingId,
                Package = booking.TravelPackage,
                SelectedHotel = booking.Hotel,
                SelectedFlight = booking.Flight,
                SelectedServices = booking.BookingServices.Select(bs => bs.Service).ToList(),
                Quantity = quantity
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookingSummary(BookingSummaryViewModel model)
        {
            // Get the current user's ID
            var userId = _userManager.GetUserId(User);

            // Find the booking based on BookingId, UserId, and Status == "Pending"
            var booking = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .FirstOrDefaultAsync(b => b.BookingId == model.BookingId
                                        && b.UserId == userId
                                        && b.Status == "Pending");

            if (booking == null)
            {
                return NotFound();
            }

            // Calculate the total cost
            decimal? totalCost = booking?.TravelPackage?.Price * model.Quantity;

            // Decrease the availability for the hotel (1 room per 2 people) and add to total cost
            if (booking?.Hotel != null)
            {
                int roomsRequired = (int)Math.Ceiling(model.Quantity / 2.0);
                booking.Hotel.Availability -= roomsRequired;
                totalCost += booking.Hotel.PricePerNight * roomsRequired;
            }

            // Decrease the availability for the flight and add to total cost
            if (booking?.Flight != null)
            {
                booking.Flight.Availability -= model.Quantity;
                totalCost += booking.Flight.Price * model.Quantity;
            }

            // Add the cost of each selected service
            foreach (var service in booking.BookingServices.Select(bs => bs.Service))
            {
                totalCost += service.Price * model.Quantity;
            }

            // Update the booking's total cost
            booking.TotalCost = (decimal)totalCost;

            // Update the booking status to confirmed
            booking.Status = "Confirmed";

            // Decrease the availability for the package
            booking.TravelPackage.Availability -= model.Quantity;

            // Save changes to the database
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Trip Booked Successfully.";

            // Redirect to the confirmation page or home page
            return RedirectToAction("Index", "Home");
        }


    }
}

