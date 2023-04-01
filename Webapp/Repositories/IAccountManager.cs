using Microsoft.AspNetCore.Identity;

namespace Webapp.Repositories;

public interface IAccountManager
{
    public Task<SignInResult> Login(string username, string password);
    public Task Logout();
}
