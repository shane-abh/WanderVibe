using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WanderVibe.Controllers.Admin
{
    public class AdminController : Controller
    {
        // [Authorize(Roles = "admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
