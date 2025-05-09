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
        // validate company (must match the current user if not admin)
        var newUserCompany = await dbContext.Companies
            .AsTracking()
            .SingleOrDefaultAsync(x => x.Id == req.CompanyId, ct);
        if (newUserCompany is null)
        {
            ThrowError(x => x.CompanyId, "Invalid company id");
        }

        var self = await userService.GetUserFromJwtAsync(ct);
        if (!userService.HasPermission(PermissionName.CreateUser) && self.CompanyId != newUserCompany.Id)
        {
            ThrowError(x => x.CompanyId, "Not authorized to create user outside of company", StatusCodes.Status403Forbidden);
        }

        var accessToken = await authService.GetAccessTokenAsync(ct);

        // confirm the requested role exists
        var desiredUserAuth0RoleId = await authService.GetRoleIdForRoleNameAsync(accessToken, req.Role.ToString(), ct);
        if (desiredUserAuth0RoleId is null)
        {
            logger.LogWarning("Requested non-existent role: {requestedRole}.", req.Role);
            ThrowError("Requested role does not exist", 400);
        }

        // create the Auth0 user and assign the Auth0 role
        var newAuth0UserId = await authService.CreateUserAsync(accessToken, req.Email, $"{req.FirstName} {req.LastName}", ct);
        await authService.AssignRoleToUserAsync(accessToken, newAuth0UserId, desiredUserAuth0RoleId, CancellationToken.None);

        // Auth0 doesn't support creating a user account without a password and having the user
        // create a password as part of the email verification. So we have to create a password
        // as part of creating their account, and then immediately send them the reset email
        // which will act as a creation / set password flow.
        await authService.ResetPasswordAsync(accessToken, req.Email, CancellationToken.None);

        // create the DB user (and assign the DB role)
        await SaveUserToDatabaseAsync(req, newAuth0UserId, CancellationToken.None);
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
            LanguageId = req.LanguageId,
        };

        await dbContext.Users.AddAsync(user, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}