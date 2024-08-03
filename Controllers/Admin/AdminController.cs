using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WanderVibe.Controllers.Admin
{

    
    [Route("admin/")]
    public class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
