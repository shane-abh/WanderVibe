using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WanderVibe.Controllers.Admin
{

    
    [Route("admin/")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {        
        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
