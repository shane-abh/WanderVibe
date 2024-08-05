using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WanderVibe.Controllers.Admin
{

    
    [Route("admin/")]
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
