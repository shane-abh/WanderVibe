using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Client
{
    public class HomeController : Controller
    {
        private readonly TravelDbContext _context;

        private static string GetBookingStatus(DateTime startDate, DateTime endDate)
        {
            DateTime currentDate = DateTime.Now;
            if (currentDate < startDate)
            {
                return "Booked";
            }
            else if (currentDate >= startDate && currentDate <= endDate)
            {
                return "On Going";
            }
            else
            {
                return "Completed";
            }
        }

        public HomeController(TravelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var packages = _context.TravelPackages.ToList();
            return View(packages);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Booking(int pageNumber = 1, int pageSize = 5)
        {
            // Get the logged-in user's ID
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve the bookings for the logged-in user
            var bookingsQuery = _context.Bookings
                .Where(b => b.UserId == userId && b.Status == "Confirmed")
                .Include(b => b.TravelPackage)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .OrderByDescending(b => b.BookingDate);

            // Calculate total bookings and pages
            int totalBookings = bookingsQuery.Count();
            int totalPages = (int)Math.Ceiling(totalBookings / (double)pageSize);

            // Fetch the bookings for the current page
            var bookings = bookingsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Process the results
            var bookingViewModels = bookings.Select(b => new UserBookingViewModel
            {
                PackageName = b.TravelPackage.PackageName,
                From = b.TravelPackage.DestinationFrom,
                To = b.TravelPackage.DestinationTo,
                StartDate = b.TravelPackage.StartDate,
                EndDate = b.TravelPackage.EndDate,
                HotelName = b.Hotel.HotelName,
                HotelEmail = b.Hotel.Email,
                HotelPhoneNumber = b.Hotel.Contact,
                FlightNumber = b.Flight.FlightNumber,
                Services = b.BookingServices.Select(bs => bs.Service.Name).ToList(),
                Status = GetBookingStatus(b.TravelPackage.StartDate, b.TravelPackage.EndDate)
            }).ToList();

            var viewModel = new PaginatedUserBookingViewModel
            {
                Bookings = bookingViewModels,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Packages(string? selectedFrom, string? selectedTo, DateTime? selectedDate)
        {
            var packages = _context.TravelPackages.AsQueryable();

            if (!string.IsNullOrEmpty(selectedFrom))
            {
                packages = packages.Where(p => p.DestinationFrom == selectedFrom);
            }

            if (!string.IsNullOrEmpty(selectedTo))
            {
                packages = packages.Where(p => p.DestinationTo == selectedTo);
            }

            if (selectedDate.HasValue)
            {
                packages = packages.Where(p => p.StartDate == selectedDate.Value);
            }

            var uniqueDestinationsFrom = _context.TravelPackages
                .Select(p => p.DestinationFrom)
                .Distinct()
                .ToList();

            var uniqueDestinationsTo = _context.TravelPackages
                .Select(p => p.DestinationTo)
                .Distinct()
                .ToList();


            var model = new PackagesViewModel
            {
                Packages = packages.ToList(),
                UniqueDestinationsFrom = uniqueDestinationsFrom,
                UniqueDestinationsTo = uniqueDestinationsTo,
                SelectedFrom = selectedFrom,
                SelectedTo = selectedTo,
                SelectedDate = selectedDate,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult PackageDetail(int id)
        {
            var package = _context.TravelPackages.FirstOrDefault(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
