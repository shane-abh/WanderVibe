using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using WanderVibe;
using WanderVibe.Models;
using WanderVibe.Data;
using WanderVibe.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database connection services
builder.Services.AddDbContext<TravelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity services
builder.Services.AddDefaultIdentity<UserProfile>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // For role
    .AddEntityFrameworkStores<TravelDbContext>();

// Other services
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache(); // Required for caching
builder.Services.AddSingleton<ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Serve static files from the Server/Upload directory
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Server", "Upload")),
    RequestPath = "/Server/Upload"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LowercaseUrlMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Map Razor Pages

// Ensure roles are created and admin user is seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRolesAndAdminAsync(services);
}

app.Run();
