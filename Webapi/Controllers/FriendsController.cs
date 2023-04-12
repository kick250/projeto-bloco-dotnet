using ApplicationBusiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Infrastructure.Exceptions;
using Repository;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FriendsController : AuthorizedController
{
    private UsersService UsersService { get; set; }

    public FriendsController(HomeRepairContext context, UsersService usersService) : base(context)
    {
        UsersService = usersService;
    }

    public IActionResult Index()
    {
        try
        {
            User user = CurrentUser();

            return Ok(user.Friends);
        } catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost("{email}")]
    public IActionResult Create(string email)
    {
        try
        {
            User user = CurrentUser();
            User Friend = UsersService.GetByEmail(email);

            UsersService.AddFriend(user, Friend);

            return Ok(new { Message = "Amigo adicionado" });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        try
        {
            User user = CurrentUser();
            User Friend = UsersService.GetByEmail(email);

            UsersService.RemoveFriend(user, Friend);

            return Ok(new { Message = "Amigo Removido" });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    
}