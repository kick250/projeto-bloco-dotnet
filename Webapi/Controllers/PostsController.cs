using ApplicationBusiness.Services;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private PostsService PostsService { get; set; }

    public PostsController(PostsService postsService)
    {
        PostsService = postsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        throw new NotImplementedException();
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

    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        throw new NotImplementedException();
    }
}
