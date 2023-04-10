using ApplicationBusiness.Services;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private CommentsService CommentsService { get; set; }

    public CommentsController(CommentsService commentsService)
    {
        CommentsService = commentsService;
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
            return Ok(CommentsService.GetById(id));
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
