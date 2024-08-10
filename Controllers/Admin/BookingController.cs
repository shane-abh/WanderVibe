using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly TravelDbContext _context;

        public BookingController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: admin/booking
        [HttpGet("")]
        public IActionResult Index()
        {
            var pendingBookings = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.TravelPackage)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .Where(b => b.Status == "Pending")
                .ToList();
            return View("ViewBookings", pendingBookings);
        }

        [HttpGet("confirmed")]
        public IActionResult ViewConfirmedBookings()
        {
            var confirmedBookings = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.TravelPackage)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .Where(b => b.Status == "Confirmed")
                .ToList();

            return View("ViewConfirmedBookings", confirmedBookings);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var booking = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.TravelPackage)
                .Include(b => b.Hotel)
                .Include(b => b.Flight)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDetails = new
            {
                user = new
                {
                    firstName = booking.User?.FirstName,
                    lastName = booking.User?.LastName
                },
                package = new
                {
                    packageName = booking.TravelPackage?.PackageName,
                    destinationFrom = booking.TravelPackage?.DestinationFrom,
                    destinationTo = booking.TravelPackage?.DestinationTo,
                    price = booking.TravelPackage?.Price
                },
                hotel = new
                {
                    hotelName = booking.Hotel?.HotelName,
                    location = booking.Hotel?.Location,
                    pricePerNight = booking.Hotel?.PricePerNight
                },
                flight = new
                {
                    flightNumber = booking.Flight?.FlightNumber,
                    departureCity = booking.Flight?.DepartureCity,
                    arrivalCity = booking.Flight?.ArrivalCity,
                    price = booking.Flight?.Price
                }
            };

            return Json(bookingDetails);
        }

        // GET: admin/booking/create
        //[HttpGet("create")]
        //public IActionResult Create()
        //{
        //    ViewBag.TravelPackages = _context.TravelPackages.ToList();
        //    ViewBag.Flights = _context.Flights.ToList();
        //    ViewBag.Hotels = _context.Hotels.ToList();
        //    ViewBag.Users = _context.Users.ToList();
        //    return View();
        //}

        // POST: admin/booking/create
        //[HttpPost("create")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        booking.BookingDate = DateTime.Now;
        //        _context.Bookings.Add(booking);
        //        _context.SaveChanges();
        //        TempData["SuccessMessage"] = "Booking created successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.TravelPackages = _context.TravelPackages.ToList();
        //    ViewBag.Flights = _context.Flights.ToList();
        //    ViewBag.Hotels = _context.Hotels.ToList();
        //    ViewBag.Users = _context.Users.ToList();
        //    return View(booking);
        //}

        // GET: admin/booking/edit/{id}
        //[HttpGet("edit/{id}")]
        //public IActionResult Edit(int id)
        //{
        //    var booking = _context.Bookings
        //        .Include(b => b.User)
        //        .Include(b => b.TravelPackage)
        //        .Include(b => b.Hotel)
        //        .Include(b => b.Flight)
        //        .FirstOrDefault(b => b.BookingId == id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.TravelPackages = _context.TravelPackages.ToList();
        //    ViewBag.Flights = _context.Flights.ToList();
        //    ViewBag.Hotels = _context.Hotels.ToList();
        //    ViewBag.Users = _context.Users.ToList();
        //    return View(booking);
        //}

        // POST: admin/booking/edit/{id}
        //[HttpPost("edit/{id}")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Bookings.Update(booking);
        //        _context.SaveChanges();
        //        TempData["SuccessMessage"] = "Booking updated successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.TravelPackages = _context.TravelPackages.ToList();
        //    ViewBag.Flights = _context.Flights.ToList();
        //    ViewBag.Hotels = _context.Hotels.ToList();
        //    ViewBag.Users = _context.Users.ToList();
        //    return View(booking);
        //}

        // POST: admin/booking/delete/{id}
        //[HttpPost("delete/{id}")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    var booking = _context.Bookings.Find(id);
        //    if (booking != null)
        //    {
        //        _context.Bookings.Remove(booking);
        //        _context.SaveChanges();
        //        TempData["SuccessMessage"] = "Booking deleted successfully!";
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
