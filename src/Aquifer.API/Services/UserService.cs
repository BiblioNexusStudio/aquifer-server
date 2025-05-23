﻿using System.Security.Claims;
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
    UserRole GetUserRoleFromJwt();
    bool HasPermission(string permission);
    Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken);
    Task<UserEntity> GetUserWithCompanyLanguagesFromJwtAsync(CancellationToken cancellationToken);
}

public class UserService(AquiferDbContext dbContext, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private string ProviderId => httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    public async Task<UserEntity> GetUserFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users.AsTracking().SingleAsync(u => u.ProviderId == ProviderId && u.Enabled, cancellationToken);
    }

    public async Task<UserEntity> GetUserWithCompanyUsersFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsTracking()
            .Include(x => x.Company)
            .ThenInclude(x => x.Users)
            .Include(x => x.Company)
            .ThenInclude(x => x.CompanyReviewers)
            .SingleAsync(u => u.ProviderId == ProviderId && u.Enabled, cancellationToken);
    }

    public List<string> GetAllJwtRoles()
    {
        return httpContextAccessor.HttpContext?.User.FindAll(Permissions.RolesClaim).Select(c => c.Value).ToList() ?? [];
    }

    public UserRole GetUserRoleFromJwt()
    {
        var jwtRoles = GetAllJwtRoles();

        Enum.TryParse(jwtRoles.FirstOrDefault(), true, out UserRole userRole);
        return userRole;
    }

    public List<string> GetAllJwtPermissions()
    {
        return httpContextAccessor.HttpContext?.User.FindAll(Permissions.PermissionsClaim).Select(c => c.Value).ToList() ?? [];
    }

    public bool HasPermission(string permission)
    {
        return httpContextAccessor.HttpContext?.User.HasClaim(c => c.Type == Permissions.PermissionsClaim && c.Value == permission) ??
            false;
    }

    public async Task<bool> ValidateNonNullUserIdAsync(int? userId, CancellationToken cancellationToken)
    {
        return userId is null ||
            await dbContext.Users.AsTracking().SingleOrDefaultAsync(u => u.Id == userId && u.Enabled, cancellationToken) is not null;
    }

    public async Task<UserEntity> GetUserWithCompanyFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsTracking()
            .Include(x => x.Company)
            .SingleAsync(u => u.ProviderId == ProviderId && u.Enabled, cancellationToken);
    }

    public async Task<UserEntity> GetUserWithCompanyLanguagesFromJwtAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsTracking()
            .Include(x => x.Company)
            .ThenInclude(x => x.CompanyLanguages)
            .SingleAsync(u => u.ProviderId == ProviderId && u.Enabled, cancellationToken);
    }
}