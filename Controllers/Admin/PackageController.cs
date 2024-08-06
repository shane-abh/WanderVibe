using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WanderVibe.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PackageController : Controller
    {
        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PackageController(TravelDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(TravelPackage travelPackage, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Server", "Upload");
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    travelPackage.ImageUrl = fileName; // Store only the file name in the database
                }

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

        // POST: admin/package/edit/{id}]
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TravelPackage travelPackage, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Server", "Upload");
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    travelPackage.ImageUrl = fileName; // Store only the file name in the database
                }

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
