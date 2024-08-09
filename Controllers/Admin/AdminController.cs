using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{

    
    [Route("admin/")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly TravelDbContext _context;

        public AdminController(TravelDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Dashboard()
        {
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalPackages = _context.TravelPackages.Count();
            ViewBag.TotalBookings = _context.Bookings.Count();
            ViewBag.TotalHotels = _context.Hotels.Count();
            ViewBag.TotalFlights = _context.Flights.Count();
            ViewBag.TotalService = _context.Services.Count();

            var recentBookings = _context.Bookings
                                 .Include(b => b.User)
                                 .Include(b => b.TravelPackage)
                                 .OrderByDescending(b => b.BookingDate)
                                 .Take(7)
                                 .ToList();

            return View("Dashboard", recentBookings);
        }
    }
}
