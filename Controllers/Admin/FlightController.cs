using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class FlightController : Controller
    {
        private readonly TravelDbContext _context;

        public FlightController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: admin/flight
        [HttpGet("")]
        public IActionResult Index()
        {
            var flights = _context.Flights.ToList();
            return View(flights);
        }

        // GET: admin/flight/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/flight/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Flight '{flight.FlightNumber}' added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: admin/flight/edit/{id}
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: admin/flight/edit/{id}]
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Update(flight);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Flight '{flight.FlightNumber}' updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // POST: admin/flight/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Flight '{flight.FlightNumber}' deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
