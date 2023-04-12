using ApplicationBusiness.Services;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private UsersService Service { get; set; }

    public UsersController(UsersService service)
    {
        Service = service;
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(user);

        try
        {
            Service.Create(user);

            return Created("", user);
        } catch (UsernameInUseException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
