using Webapp.Models;

namespace Webapp.Helpers;

public class SessionHelper
{
    HttpContext HttpContext { get; set; }

    public SessionHelper(HttpContext httpContext)
    {
        HttpContext = httpContext;
    }

    public bool TokenIsPresent()
    {
        return !string.IsNullOrEmpty(GetToken());
    }

    public Account GetCurrentAccount()
    {
        return new Account(GetToken());
    }

    public string GetToken()
    {
        return HttpContext.Session.GetString(Account.SESSION_TOKEN_KEY) ?? "";
    }
}
