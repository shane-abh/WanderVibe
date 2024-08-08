using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Client
{
    public class HomeController : Controller
    {
        private readonly TravelDbContext _context;

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
                SelectedDate = selectedDate
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
