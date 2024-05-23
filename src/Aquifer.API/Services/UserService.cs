using System.Security.Claims;
using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Services;

public interface IUserService
{
    Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken);
    Task<UserEntity> GetUserWithCompanyUsersFromJwtAsync(CancellationToken cancellationToken);
    Task<UserEntity> GetUserWithCompanyFromJwtAsync(CancellationToken cancellationToken);
    List<string> GetAllJwtPermissions();
    List<string> GetAllJwtRoles();
    bool HasPermission(string permission);
    Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken);
}

public class UserService(AquiferDbContext dbContext, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private string ProviderId => httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    public async Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users.SingleAsync(u => u.ProviderId == ProviderId, cancellationToken);
    }

    public async Task<UserEntity> GetUserWithCompanyUsersFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users.Include(x => x.Company).ThenInclude(x => x.Users)
            .SingleAsync(u => u.ProviderId == ProviderId, cancellationToken);
    }

    public List<string> GetAllJwtRoles()
    {
        return httpContextAccessor.HttpContext?.User.FindAll(Constants.RolesClaim).Select(c => c.Value)
            .ToList() ?? [];
    }

    public List<string> GetAllJwtPermissions()
    {
        return httpContextAccessor.HttpContext?.User.FindAll(Constants.PermissionsClaim).Select(c => c.Value)
            .ToList() ?? [];
    }

    public bool HasPermission(string permission)
    {
        return httpContextAccessor.HttpContext?.User.HasClaim(c =>
                c.Type == Constants.PermissionsClaim && c.Value == permission) ??
            false;
    }

    public async Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken)
    {
        return userId is null || await dbContext.Users.FindAsync([userId], cancellationToken) is not null;
    }

    public async Task<UserEntity> GetUserWithCompanyFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users.Include(x => x.Company).SingleAsync(u => u.ProviderId == ProviderId, cancellationToken);
    }
}