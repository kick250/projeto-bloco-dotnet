using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;
public class AuthenticationAPI : IAPI
{
    public AuthenticationAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public string Authenticate(string email, string password)
    {
        var request = new
        {
            email = email,
            password = password
        };

        var response = Post("/Authentication", request).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        var definition = new { Token = "" };

        var result = JsonConvert.DeserializeAnonymousType(jsonResult, definition);

        if (result == null || result.Token == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result.Token;
    }
}
