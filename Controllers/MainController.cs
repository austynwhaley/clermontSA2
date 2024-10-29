using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clermontSA2.Models;
using System.Collections.Generic;

namespace clermontSA2.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly ClermontDb _context;

        public MainController(ILogger<MainController> logger, ClermontDb context)
        {
            _logger = logger;
            _context = context;
        }

        // users -> views actions
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            var users = await _context.Users.ToListAsync();
            var totalUsers = users.Count();

            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var paginatedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new UserListViewModel
            {
                Users = paginatedUsers,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View("Main", model);
        }

        public async Task<IActionResult> Admin()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // err action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // routes
        public IActionResult Main()
        {
            return RedirectToAction("Index", "");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Detail/Detail.cshtml", user);
        }

        public IActionResult AdminView()
        {
            var users = _context.Users.ToList();
            return View("~/Views/Admin/Admin.cshtml", users);
        }

        // update action
        [HttpPost]
        public IActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.Phone = user.Phone;
                    existingUser.Address = user.Address;
                    existingUser.PictureUrl = user.PictureUrl;

                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "User not found." });
            }
            // form validation
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = string.Join(", ", errors) });
        }

        // delete action
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
