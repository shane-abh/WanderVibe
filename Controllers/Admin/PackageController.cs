using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    public class PackageController : Controller
    {
        private readonly TravelDbContext _context;

        // @Desc - Constructor to inject TravelDbContext.
        public PackageController(TravelDbContext context)
        {
            _context = context;
        }

        // @Desc - Get request for view.
        public IActionResult Index()
        {
            var packages = _context.TravelPackages.ToList(); // Fetch all packages
            return View(packages);
        }

        // @Desc - Get request for package form.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // @Desc - Post request for create (save) package.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                // Save travelPackage to the database
                _context.TravelPackages.Add(travelPackage);
                _context.SaveChanges(); // Save changes to the database

                // Set success message in TempData
                TempData["SuccessMessage"] = $"Package '{travelPackage.PackageName}' added successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }
    }
}
