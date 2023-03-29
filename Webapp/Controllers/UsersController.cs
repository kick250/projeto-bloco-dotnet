using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
