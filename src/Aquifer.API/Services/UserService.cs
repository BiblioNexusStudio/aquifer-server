using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Services;

public interface IUserService
{
    Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken);
    List<string> GetAllPermissions();
    bool HasClaim(string permission);
    Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken);
}

public class UserService(AquiferDbContext _dbContext, IHttpContextAccessor _httpContextAccessor) : IUserService
{
    public async Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken)
    {
        string providerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        return await _dbContext.Users.SingleAsync(u => u.ProviderId == providerId, cancellationToken);
    }

    public List<string> GetAllPermissions()
    {
        return _httpContextAccessor.HttpContext?.User.FindAll(Constants.PermissionsClaim).Select(c => c.Value)
                   .ToList() ??
               new List<string>();
    }

    public bool HasClaim(string permission)
    {
        return _httpContextAccessor.HttpContext?.User.HasClaim(c =>
                   c.Type == Constants.PermissionsClaim && c.Value == permission) ??
               false;
    }

    public async Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken)
    {
        return userId is null || await _dbContext.Users.FindAsync(userId, cancellationToken) is not null;
    }
}