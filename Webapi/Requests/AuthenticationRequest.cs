using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Webapi.Requests;

public class AuthenticationRequest
{
    [Required(ErrorMessage = "É necessário o atributo 'email' nessa requisição"),
     EmailAddress(ErrorMessage = "O email precisa estar em um formato valido"),
     JsonPropertyName("email")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "É necessário o atributo 'password' nessa requisição"),
     JsonPropertyName("password")]
    public string? Password { get; set; }

    public string GetEmail()
    {
        if (Email == null)
            throw new RequiredParameterNotPresent("email");

        return Email;
    }

    public string GetPassword()
    {
        if (Password == null)
            throw new RequiredParameterNotPresent("password");

        return Password;
    }
}
