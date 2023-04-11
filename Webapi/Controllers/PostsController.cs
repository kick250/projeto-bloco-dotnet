using ApplicationBusiness.Services;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController : AuthorizedController
{
    private PostsService PostsService { get; set; }

    public PostsController(HomeRepairContext context, PostsService postsService) : base(context)
    {
        PostsService = postsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        User user = CurrentUser();

        return Ok(PostsService.GetPostsFor(user));
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try
        {
            return Ok(PostsService.GetById(id));
        } catch (PostNotFoundException ex)
        {
            return NotFound(new { Error = ex.GetMessage() });
        }
    }

    //[HttpPost]
    //public IActionResult Post([FromBody] string value)
    //{
    //    throw new NotImplementedException();
    //}
}
