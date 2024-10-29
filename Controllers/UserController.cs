using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly RandomUser _randomUserService;
    private readonly ClermontDb _dbContext;

    public UserController(RandomUser randomUserService, ClermontDb dbContext)
    {
        _randomUserService = randomUserService;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> CreateRandomUser()
    {
        var user = await _randomUserService.FetchRandomUserDataAsync();
        if (user != null)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View("Error");
    }
}

