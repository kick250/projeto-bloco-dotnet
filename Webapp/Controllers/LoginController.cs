using Microsoft.AspNetCore.Mvc;
using Webapp.Models;
using Webapp.Repositories;

namespace Webapp.Controllers;
public class LoginController : Controller
{
    IAccountManager AccountManager { get; set; }

    public LoginController(IAccountManager accountManager)
    {
        AccountManager = accountManager;
    }

    public ActionResult New([FromQuery] string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    public ActionResult Create([FromForm] Account account, [FromQuery] string? returnUrl) 
    {
        if (!ModelState.IsValid)
            return View("New", account);

        var result = AccountManager.Login(account.GetEmail(), account.GetPassword()).Result;

        if (!result.Succeeded)
        {
            ViewBag.Error = "Email ou senha incorretos.";
            return View("new", account);
        }

        if (String.IsNullOrEmpty(returnUrl))
            return Redirect("/");

        return Redirect(returnUrl);
    }
}
