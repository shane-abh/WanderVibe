using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    public class PackageController : Controller
    {
        private readonly TravelDbContext _context;

        public PackageController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: admin/package
        [HttpGet("")]
        public IActionResult Index()
        {
            var packages = _context.TravelPackages.ToList();
            return View(packages);
        }

        // GET: admin/package/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/package/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                _context.TravelPackages.Add(travelPackage);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Package '{travelPackage.PackageName}' added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }

        // GET: admin/package/edit/{id}
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var travelPackage = _context.TravelPackages.Find(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        // POST: admin/package/edit/{id}
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                _context.TravelPackages.Update(travelPackage);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Package '{travelPackage.PackageName}' updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }

        // POST: admin/package/delete/{id}
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var travelPackage = _context.TravelPackages.Find(id);
            if (travelPackage != null)
            {
                _context.TravelPackages.Remove(travelPackage);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Package '{travelPackage.PackageName}' deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
