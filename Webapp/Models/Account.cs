using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace Webapp.Models;
public class Account
{
    public string? Token { get; set; }

    [Required(ErrorMessage = "O email é obrigatório."),
     EmailAddress(ErrorMessage = "O email deve estar no formato correto.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigatório.")]
    public string? Password { get; set; }

    public string Id
    {
        get
        {
            if (string.IsNullOrEmpty(Token)) return "0";
            return GetKeyFromToken("sub");
        }
    }

    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(Token)) return "";

            return GetKeyFromToken("name");
        }
    }

    public string GetEmail()
    {
        return Email ?? "";
    }

    public string GetPassword()
    {
        return Password ?? "";
    }

    #region private

    private string GetKeyFromToken(string type)
    {
        return DecodeToken().Claims.First(x => x.Type == type).Value;
    }

    private JwtSecurityToken DecodeToken()
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonResult = handler.ReadJwtToken(Token);

        return jsonResult as JwtSecurityToken;
    }

    #endregion
}
