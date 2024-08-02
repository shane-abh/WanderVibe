using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    public class PackageController : Controller
    {
        // @Desc - Get request for view.
        public IActionResult Index()
        {
            return View();
        }

        // @Desc - Get request for package form.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // @Desc - Post request for create package.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                // Save travelPackage to the database
                // _context.TravelPackages.Add(travelPackage);
                // _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }
    }
}
