using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly TravelDbContext _context;

        public ServiceController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: admin/service
        [HttpGet("")]
        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        // GET: admin/service/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/service/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Service '{service.Name}' added successfully!";
                return RedirectToAction(nameof(Index));
            }           
            return View(service);
        }

        // GET: admin/service/edit/{id}
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            // Fetch unique destinations from TravelPackage
            ViewBag.Destinations = _context.TravelPackages
                .Select(tp => tp.DestinationTo)
                .Distinct()
                .ToList();

            return View(service);
        }

        // POST: admin/service/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Service '{service.Name}' updated successfully!";
                return RedirectToAction(nameof(Index));
            }           

            return View(service);
        }

        // POST: admin/service/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Service '{service.Name}' deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Service not found.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
