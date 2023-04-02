using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Webapp.Helpers;
using Webapp.Models;

namespace Webapp.Controllers;

[Authorize]
public class FriendsController : Controller
{
    FriendsAPI FriendsAPI { get; set; }
    SessionHelper SessionHelper { get; set; }

    public FriendsController(FriendsAPI friendsAPI, SessionHelper sessionHelper)
    {
        FriendsAPI = friendsAPI;
        SessionHelper = sessionHelper;
    }

    public IActionResult Index()
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        List<User> friends = FriendsAPI.GetFriendsOf(account);

        return View(friends);
    }

    public IActionResult New()
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        return View();
    }

    [HttpPost]
    public IActionResult AddFriend(string email)
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        if (!ModelState.IsValid)
        {
            ViewBag.Error = "O email é necessário.";
            return View("New", email);
        }

        try
        {
            FriendsAPI.AddFriend(account, email);

            return RedirectToAction("Index");
        } catch (APIErrorException ex)
        {
            ViewBag.Error = ex.GetMessage();
            return View("New", email);
        }
    }

    [HttpGet]
    public IActionResult Delete([FromQuery] string friendEmail)
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        return View("Delete", friendEmail);
    }

    [HttpPost]
    public IActionResult RemoveFriend([FromQuery] string friendEmail)
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        if (!ModelState.IsValid)
        {
            ViewBag.Error = "O email é necessário.";
            return View("new", friendEmail);
        }

        try
        {
            FriendsAPI.RemoveFriend(account, friendEmail);

            return RedirectToAction("Index");
        }
        catch (APIErrorException ex)
        {
            ViewBag.Error = ex.GetMessage();
            return View("new", friendEmail);
        }
    } 

    #region private

    private Account? GetAccount()
    {
        if (!SessionHelper.TokenIsPresent())
            return null;  

        return SessionHelper.GetCurrentAccount(); ;
    }

    #endregion
}
