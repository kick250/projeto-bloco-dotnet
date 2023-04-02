using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Webapp.APIs;
using Webapp.Models;

namespace Webapp.Repositories;

public class AccountManager : IAccountManager
{
    private SignInManager<Account> SignInManager { get; set; }
    private AuthenticationAPI AuthenticationAPI { get; set; }
    private HttpContext? HttpContext { get; set; }

    public AccountManager(SignInManager<Account> signInManager, AuthenticationAPI authenticationAPI, IHttpContextAccessor httpContextAccessor)
    {
        SignInManager = signInManager;
        AuthenticationAPI = authenticationAPI;
        HttpContext = httpContextAccessor.HttpContext;
    }

    public async Task<SignInResult> Login(string username, string password) 
    {
        try
        {
            string token = AuthenticationAPI.Authenticate(username, password);

            Account account = new Account
            {
                Email = username,
                Password = password,
                Token = token
            };

            await SignInManager.SignInAsync(account, true);

            if (HttpContext != null)
                HttpContext.Session.SetString(Account.SESSION_TOKEN_KEY, token);

            return SignInResult.Success;
        } catch (APIErrorException) 
        {
            return SignInResult.Failed;
        }
    }

    public async Task Logout()
    {
        await SignInManager.SignOutAsync();
    }
}
