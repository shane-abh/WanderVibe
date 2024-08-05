using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WanderVibe.Models;

namespace WanderVibe.Data
{
    public class RoleInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();

            string[] roleNames = { "Admin", "Client" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var powerUser = new UserProfile
            {
                UserName = "WanderVibe",
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
            };

            string userPassword = "Admin@123456";
            var user = await userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(powerUser, "Admin");
                }
            }
        }
    }
}
