using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Webapp.Models;
using Webapp.Helpers;

namespace Webapp.Controllers;

[Authorize]
public class PostsController : Controller
{
    PostsAPI PostsAPI { get; set; }
    SessionHelper SessionHelper { get; set; }

    public PostsController(PostsAPI postsAPI, SessionHelper sessionHelper)
    {
        PostsAPI = postsAPI;
        SessionHelper = sessionHelper;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Details(int id)
    {
        Account? account = GetAccount();
        if (account == null) return Redirect("/Login/logout");

        Post post = PostsAPI.GetById(account, id);

        return View(post);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
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
