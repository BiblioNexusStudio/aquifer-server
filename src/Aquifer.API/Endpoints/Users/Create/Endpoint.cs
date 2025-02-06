using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.Create;

public class Endpoint(AquiferDbContext dbContext, IAuth0Service authService, ILogger<Endpoint> logger, IUserService userService)
    : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/users/create");
        Permissions(PermissionName.CreateUser, PermissionName.CreateUsersInCompany);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await CreateUserAsync(req, ct);
        await SendNoContentAsync(ct);
    }

    private async Task CreateUserAsync(Request req, CancellationToken ct)
    {
        await ValidateCompanyIdAsync(req.CompanyId, ct);
        var accessToken = await authService.GetAccessTokenAsync(ct);
        var desiredUserAuth0RoleId = await GetRoleIdAsync(req, accessToken, ct);
        var newAuth0UserId = await CreateAuth0UserAsync(req, accessToken, ct);
        await AssignAuth0RoleAsync(accessToken, newAuth0UserId, desiredUserAuth0RoleId, ct);
        await SendPasswordResetAsync(accessToken, req.Email, ct);

        await SaveUserToDatabaseAsync(req, newAuth0UserId, ct);
    }

    private async Task ValidateCompanyIdAsync(int companyId, CancellationToken ct)
    {
        var newUserCompany = await dbContext.Companies
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id == companyId, ct);
        if (newUserCompany is null)
        {
            ThrowError(x => x.CompanyId, "Invalid company id");
        }

        var self = await userService.GetUserFromJwtAsync(ct);
        if (!userService.HasPermission(PermissionName.CreateUser) && self.CompanyId != newUserCompany.Id)
        {
            ThrowError(x => x.CompanyId, "Not authorized to create user outside of company", StatusCodes.Status403Forbidden);
        }
    }

    private async Task<string> GetRoleIdAsync(Request req, string accessToken, CancellationToken ct)
    {
        var roleId = await authService.GetRoleIdForRoleNameAsync(accessToken, req.Role.ToString(), ct);

        if (roleId is null)
        {
            logger.LogWarning("Requested non-existent role: {requestedRole}.", req.Role);
            ThrowError("Requested role does not exist", 400);
        }

        return roleId;
    }

    private async Task<string> CreateAuth0UserAsync(Request req, string accessToken, CancellationToken ct)
    {
        return await authService.CreateUserAsync(accessToken, req.Email, $"{req.FirstName} {req.LastName}", ct);
    }

    private async Task AssignAuth0RoleAsync(string accessToken, string userId, string roleId, CancellationToken ct)
    {
        await authService.AssignRoleToUserAsync(accessToken, userId, roleId, ct);
    }

    private async Task SendPasswordResetAsync(string accessToken, string email, CancellationToken ct)
    {
        // Auth0 doesn't support creating a user account without a password and having the user
        // create a password as part of the email verification. So we have to create a password
        // as part of creating their account, and then immediately send them the reset email
        // which will act as a creation / set password flow.
        await authService.ResetPasswordAsync(accessToken, email, ct);
    }

    private async Task SaveUserToDatabaseAsync(Request req, string providerUserId, CancellationToken ct)
    {
        var user = new UserEntity
        {
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            ProviderId = providerUserId,
            CompanyId = req.CompanyId,
            Role = req.Role,
            Enabled = true,
            AquiferNotificationsEnabled = true,
        };

        await dbContext.Users.AddAsync(user, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}