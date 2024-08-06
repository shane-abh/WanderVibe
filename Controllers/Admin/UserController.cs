using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WanderVibe.Models;

namespace WanderVibe.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<UserProfile> _userManager;

        public UserController(UserManager<UserProfile> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Clients()
        {
            var users = _userManager.Users.ToList();
            var clients = new List<UserProfile>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Client"))
                {
                    clients.Add(user);
                }
            }

            return View(clients);
        }

        [HttpGet("admins")]
        public async Task<IActionResult> Admins()
        {
            var users = _userManager.Users.ToList();
            var admins = new List<UserProfile>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    admins.Add(user);
                }
            }

            return View(admins);
        }
    }
}
