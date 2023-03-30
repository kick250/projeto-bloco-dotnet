using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Infrastructure.Exceptions;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    private UsersAPI API { get; set; }

    public UsersController(UsersAPI api)
    {
        API = api;
    }

    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid) return View("New", user);

        try
        {
            API.create(user);
        }
        catch (APIErrorException ex)
        {
            ViewBag.Error = ex.GetMessage();
            return View("New", user);

        }

        return Redirect("/");
    }
}
