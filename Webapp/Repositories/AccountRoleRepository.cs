﻿using Microsoft.AspNetCore.Identity;
using Webapp.Models;

namespace Webapp.Repositories;

public class AccountRoleRepository : IRoleStore<AccountRole>
{
    public void Dispose()
    {
    }

    #region Not in use
    public Task<IdentityResult> CreateAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    

    public Task<AccountRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AccountRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedRoleNameAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetRoleNameAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(AccountRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(AccountRole role, string? roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(AccountRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion
}
