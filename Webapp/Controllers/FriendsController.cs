using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

[Authorize]
public class FriendsController : AuthorizedController
{
    FriendsAPI FriendsAPI { get; set; }

    public FriendsController(FriendsAPI friendsAPI)
    {
        FriendsAPI = friendsAPI;
    }

    protected override void SetAPIToken()
    {
        FriendsAPI.AddToken(SessionToken);
    }

    public IActionResult Index()
    {
        List<User> friends = FriendsAPI.GetFriends();

        return View(friends);
    }

    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddFriend(string email)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Error = "O email é necessário.";
            return View("New", email);
        }

        try
        {
            FriendsAPI.AddFriend(email);

            return RedirectToAction("Index");
        } catch (APIErrorException ex)
        {
            ViewBag.Error = ex.Message;
            return View("New", email);
        }
    }

    [HttpGet]
    public IActionResult Delete([FromQuery] string friendEmail)
    {
        return View("Delete", friendEmail);
    }

    [HttpPost]
    public IActionResult RemoveFriend([FromQuery] string friendEmail)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Error = "O email é necessário.";
            return View("new", friendEmail);
        }

        try
        {
            FriendsAPI.RemoveFriend(friendEmail);

            return RedirectToAction("Index");
        }
        catch (APIErrorException ex)
        {
            ViewBag.Error = ex.Message;
            return View("new", friendEmail);
        }
    } 
}
