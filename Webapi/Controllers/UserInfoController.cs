using ApplicationBusiness.Services;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserInfoController : AuthorizedController
{
    private UsersService UsersService { get; set; }

    public UserInfoController(HomeRepairContext context, UsersService usersService) 
        : base(context)
    {
        UsersService = usersService;
    }

    [HttpGet]
    public IActionResult Details()
    {
        User user = CurrentUser();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        User user = CurrentUser();

        UsersService.Delete(user);

        return NoContent();
    }
}
