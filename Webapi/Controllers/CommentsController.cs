using ApplicationBusiness.Services;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Webapi.Requests;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : AuthorizedController
{
    private CommentsService CommentsService { get; set; }
    private PostsService PostsService { get; set; }

    public CommentsController(HomeRepairContext context, CommentsService commentsService, PostsService postsService) 
        : base(context)
    {
        CommentsService = commentsService;
        PostsService = postsService;
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try
        {
            return Ok(CommentsService.GetById(id));
        } catch (CommentNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] CommentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

        try
        {
            Comment comment = request.GetComment();
            comment.Owner = CurrentUser();
            comment.Post = PostsService.GetById(request.GetPostId());

            CommentsService.Create(comment);

            return Ok("Esse comentário foi criado.");
        } catch (RequiredParameterNotPresent ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            CommentsService.DeleteById(id);

            return NoContent();
        } catch (CommentNotFoundException)
        {
            return NoContent();
        }
    }
}
