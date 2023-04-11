using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Webapp.Helpers;

namespace Webapp.Controllers;

public class AuthorizedController : Controller
{
    protected string? SessionToken { get; set; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        SessionHelper sessionHelper = new SessionHelper(context.HttpContext);

        if (!sessionHelper.TokenIsPresent())
        {
            string path = context.HttpContext.Request.Path;
            context.Result = Logout(path);
            return;
        }

        SessionToken = sessionHelper.GetToken();

        SetAPIToken();

        base.OnActionExecuting(context);
    }
    protected virtual void SetAPIToken() { }

    #region private

    private RedirectToRouteResult Logout(string returnUrl)
    {
        var routeParams = new RouteValueDictionary(new
        {
            controller = "Login",
            action = "Logout",
            ReturnUrl = returnUrl
        });
        return new RedirectToRouteResult(routeParams);
    }

    #endregion
}
