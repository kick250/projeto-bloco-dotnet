using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[Authorize]
public class PostsController : AuthorizedController
{
    private PostsAPI PostsAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }

    public PostsController(PostsAPI postsAPI, ImagesAPI imagesAPI)
    {
        PostsAPI = postsAPI;
        ImagesAPI = imagesAPI;
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

    public ActionResult Details(int? id)
    {
        if (id == null) return RedirectToAction(nameof(Index));

        Post post = PostsAPI.GetById(int.Parse($"{id}"));

        return View(post);
    }

    public ActionResult New()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Post post, IFormFile imageFile)
    {
        if (!ModelState.IsValid) return View(nameof(New), post);

        try
        {
            string imageUrl = ImagesAPI.UploadImage(imageFile);
            post.ImageUrl = imageUrl;
            PostsAPI.Create(post);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) 
        {
            ViewBag.Error = ex.Message;
            return View(nameof(New), post);
        }
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
        PostsAPI.DeleteById(id);

        return RedirectToAction(nameof(Index));
    }
}
