using System.Security.Claims;
using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Services;

public interface IUserService
{
    Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken);
    List<string> GetAllJwtPermissions();
    List<string> GetAllJwtRoles();
    bool HasPermission(string permission);
    Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken);
}

public class UserService(AquiferDbContext _dbContext, IHttpContextAccessor _httpContextAccessor) : IUserService
{
    public async Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken)
    {
        var providerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        return await _dbContext.Users.SingleAsync(u => u.ProviderId == providerId, cancellationToken);
    }

    public List<string> GetAllJwtRoles()
    {
        return _httpContextAccessor.HttpContext?.User.FindAll(Constants.RolesClaim).Select(c => c.Value)
            .ToList() ?? [];
    }

    public List<string> GetAllJwtPermissions()
    {
        return _httpContextAccessor.HttpContext?.User.FindAll(Constants.PermissionsClaim).Select(c => c.Value)
            .ToList() ?? [];
    }

    public bool HasPermission(string permission)
    {
        return _httpContextAccessor.HttpContext?.User.HasClaim(c =>
                   c.Type == Constants.PermissionsClaim && c.Value == permission) ??
               false;
    }

    public async Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken)
    {
        return userId is null || await _dbContext.Users.FindAsync([userId], cancellationToken) is not null;
    }
}