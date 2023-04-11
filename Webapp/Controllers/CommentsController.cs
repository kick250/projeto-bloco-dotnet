using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[Authorize]
public class CommentsController : AuthorizedController
{
    CommentsAPI CommentsAPI { get; set; }

    public CommentsController(CommentsAPI commentsAPI)
    {
        CommentsAPI = commentsAPI;
    }

    protected override void SetAPIToken()
    {
        CommentsAPI.AddToken(SessionToken);
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Details(int id)
    {
       Comment comment = CommentsAPI.GetById(id);

        return View(comment);
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
