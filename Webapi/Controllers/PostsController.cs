using ApplicationBusiness.Services;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Post post)
    {
        if (!ModelState.IsValid) return BadRequest(post);

        post.Owner = CurrentUser();

        PostsService.Create(post);

        return Created("Post Criado", new {});
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Post post)
    {
        if (!ModelState.IsValid) return BadRequest(post);

        PostsService.Update(id, post);
        return Ok("Post Atualizado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            PostsService.DeleteById(id);

            return NoContent();
        } catch (PostNotFoundException)
        {
            return NoContent();
        }
    }
}
