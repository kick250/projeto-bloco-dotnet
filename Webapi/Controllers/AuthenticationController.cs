using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Webapi.Requests;
using ApplicationBusiness.Services;

namespace Webapi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private UsersService UsersService { get; set; }
    private IConfiguration Configuration { get; set; }

    public AuthenticationController(UsersService usersService, IConfiguration configuration)
    {
        UsersService = usersService;
        Configuration = configuration;
    }

    [HttpPost]
    public IActionResult Token(AuthenticationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(request);

        User? user = UsersService.Authenticate(request.GetEmail(), request.GetPassword());

        if (user == null)
            return Unauthorized(new { Error = "Usuário e senha não existem." });

        return Ok(new { token = GetToken(user) });
    }

    #region private

    private string GetToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("sub", $"{user.Id}"),
            new Claim("userId", $"{user.Id}"),
            new Claim("email", user.GetEmail()),
            new Claim("name", user.GetName()),
            new Claim("profileImage", user.GetProfileImage())
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.Default.GetBytes(GetTokenKey());
        var securityToken = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = "home-repair-token",
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(securityToken);

        return tokenHandler.WriteToken(token);
    }

    private string GetTokenKey()
    {
        string? tokenKey = Configuration["TokenSecret"];

        if (tokenKey == null) return "";

        return tokenKey;
    }

    #endregion

}


