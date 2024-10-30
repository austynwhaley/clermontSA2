using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<RandomUser>();
builder.Services.AddDbContext<ClermontDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // Path to the login page
    });

var app = builder.Build();

// Check database connection and user count
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClermontDb>();

    try
    {
        // Check if database is accessible and count users
        var userCount = await context.Users.CountAsync();
        Console.WriteLine($"Database connected. Number of users: {userCount}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// Create the database and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClermontDb>();
    var randomUserService = services.GetRequiredService<RandomUser>();

    // Seed database
    await context.SeedDataAsync(randomUserService);
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Shared/Error");
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Main}/{action=Index}/{id?}");
});

app.Run();
