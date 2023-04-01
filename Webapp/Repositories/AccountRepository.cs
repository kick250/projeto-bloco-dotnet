using Microsoft.AspNetCore.Identity;
using Webapp.Models;

namespace Webapp.Repositories;

public class AccountRepository : IUserStore<Account>
{
    public Task<string?> GetNormalizedUserNameAsync(Account account, CancellationToken cancellationToken)
    {
        return Task.FromResult(account.Email);
    }

    public Task<string> GetUserIdAsync(Account account, CancellationToken cancellationToken)
    {
        return Task.FromResult(account.Id);
    }

    public Task<string?> GetUserNameAsync(Account account, CancellationToken cancellationToken)
    {
        return Task.FromResult(account.Email);
    }

    public void Dispose()
    {
    }

    #region Not in use
    public Task<IdentityResult> CreateAsync(Account account, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(Account account, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(Account account, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(Account account, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(Account account, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion
}
