using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;

namespace Webapp.Controllers;

public class PostsController : Controller
{
    PostsAPI PostsAPI { get; set; }

    public PostsController(PostsAPI postsAPI)
    {
        PostsAPI = postsAPI;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Details(int id)
    {
        Post post = PostsAPI.GetById(id);

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
}
