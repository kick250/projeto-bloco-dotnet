using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

public class FriendController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
