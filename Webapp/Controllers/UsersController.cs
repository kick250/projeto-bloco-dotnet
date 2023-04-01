using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Infrastructure.Exceptions;
using Webapp.Repositories;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    IAccountManager AccountManager { get; set; }
    private UsersAPI API { get; set; }

    public UsersController(UsersAPI api, IAccountManager accountManager)
    {
        API = api;
        AccountManager = accountManager;
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
            AccountManager.Login(user.GetEmail(), user.GetPassword());
        }
        catch (APIErrorException ex)
        {
            ViewBag.Error = ex.GetMessage();
            return View("New", user);
        }

        return Redirect("/");
    }
}
