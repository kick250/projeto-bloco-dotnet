using ApplicationBusiness.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Webapi.Controllers;

public class AuthorizedController : Controller
{
    private UsersService UsersService { get; set; }

    public AuthorizedController(HomeRepairContext context)
    {
        this.UsersService = new UsersService(context);
    }

    protected User CurrentUser()
    {
        return this.UsersService.GetById(GetCurrentUserId());
    }

    #region private 
    private int GetCurrentUserId()
    {
        var claims = this.User.Claims;
        var value = claims.FirstOrDefault(x => x.Type == "userId");

        if (value == null) throw new Exception("Ocorreu um erro desconhecido");

        return int.Parse(value.Value);
    }
    #endregion
}
