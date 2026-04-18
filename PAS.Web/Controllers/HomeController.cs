using Microsoft.AspNetCore.Mvc;
using PAS.Web.Models;
using System.Diagnostics;

namespace PAS.Web.Controllers;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToAction("Dashboard", "Admin");

            if (await _userManager.IsInRoleAsync(user, "Supervisor"))
                return RedirectToAction("Dashboard", "Supervisor");

            if (await _userManager.IsInRoleAsync(user, "Student"))
                return RedirectToAction("Dashboard", "Student");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}