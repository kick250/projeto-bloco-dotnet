using ApplicationBusiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Infrastructure.Exceptions;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FriendsController : ControllerBase
{
    private UsersService UsersService { get; set; }

    public FriendsController(UsersService usersService)
    {
        UsersService = usersService;
    }

    public IActionResult Index()
    {
        try
        {
            User user = UsersService.GetById(GetCurrentUserId());

            return Ok(user.Friends);
        } catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.GetMessage() });
        }
    }

    [HttpPost("{email}")]
    public IActionResult Create(string email)
    {
        try
        {
            User user = UsersService.GetById(GetCurrentUserId());
            User Friend = UsersService.GetByEmail(email);

            UsersService.AddFriend(user, Friend);

            return Ok(new { Message = "Amigo adicionado" });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.GetMessage() });
        }
    }

    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        try
        {
            User user = UsersService.GetById(GetCurrentUserId());
            User Friend = UsersService.GetByEmail(email);

            UsersService.RemoveFriend(user, Friend);

            return Ok(new { Message = "Amigo Removido" });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { Error = ex.GetMessage() });
        }
    }

    private int GetCurrentUserId()
    {
        var claims = this.User.Claims;
        var value = claims.FirstOrDefault(x => x.Type == "userId");

        if (value == null) throw new Exception("Ocorreu um erro desconhecido");

        return int.Parse(value.Value);
    }
}