using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[Authorize]
public class PostsController : AuthorizedController
{
    PostsAPI PostsAPI { get; set; }

    public PostsController(PostsAPI postsAPI)
    {
        PostsAPI = postsAPI;
    }

    protected override void SetAPIToken()
    {
        PostsAPI.AddToken(SessionToken);
    }

    public ActionResult Index()
    {
        List<Post> posts = PostsAPI.GetAll();
        return View(posts);
    }

    public ActionResult Details(int id)
    {
        Post post = PostsAPI.GetById(id);

        return View(post);
    }

    //public ActionResult Create()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public ActionResult Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}
