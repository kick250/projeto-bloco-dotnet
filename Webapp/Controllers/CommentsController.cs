using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Microsoft.AspNetCore.Authorization;
using Entities;

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

    public ActionResult New(int? postId)
    {
        if (postId == null || postId == 0) return Redirect("/");
        
        ViewBag.PostId = postId;

        return View();
    }

    [HttpPost]
    public ActionResult Create(Comment comment, int postId)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.PostId = postId;
            return View(nameof(New), comment);
        }

        try
        {
            CommentsAPI.Create(comment, postId);

            return Redirect($"/Posts/Details?Id={postId}");
        }
        catch (Exception ex) 
        {
            ViewBag.Error = ex.Message;
            ViewBag.PostId = postId;
            return View(nameof(New), comment);
        }
    }

    [HttpPost]
    public ActionResult Delete(int id, int postId)
    {
        CommentsAPI.DeleteById(id);

        return Redirect($"/Posts/Details?Id={postId}");
    }
}
