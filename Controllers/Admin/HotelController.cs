using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private readonly TravelDbContext _context;

        public HotelController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: admin/hotel
        [HttpGet("")]
        public IActionResult Index()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }

        // GET: admin/hotel/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/hotel/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Hotel '{hotel.HotelName}' added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: admin/hotel/edit/{id}
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: admin/hotel/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Hotel '{hotel.HotelName}' updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // POST: admin/hotel/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Hotel '{hotel.HotelName}' deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
