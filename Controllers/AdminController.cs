using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class AdminController : Controller
{
    private readonly ClermontDb _context;

    public AdminController(ILogger<AdminController> logger, ClermontDb context)
        {
            _context = context;
        }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var username = loginModel.Username.ToLower();
        var password = loginModel.Password.ToLower();

        if (username == "admin" && password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok();
        }

        return BadRequest("Invalid username or password");
    }


    [HttpGet]
    public IActionResult AdminView()
    {
        var users = _context.Users.ToList();
        return View("~/Views/Admin/Admin.cshtml", users);
    }

}
