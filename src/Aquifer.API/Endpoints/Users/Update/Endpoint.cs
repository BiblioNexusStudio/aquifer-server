using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.Update;

public class Endpoint(AquiferDbContext _dbContext, IAuth0Service _authService, IUserService _userService, ILogger<Endpoint> _logger)
    : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/users/{userId}");
        Permissions(PermissionName.UpdateUser, PermissionName.UpdateUsersInCompany);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        // verify user exists and is in the same company as the current user (if not an admin)
        var user = _dbContext.Users
            .AsTracking()
            .FirstOrDefault(u => u.Id == req.UserId);
        if (user is null)
        {
            ThrowError(
                x => x.UserId,
                $"User with ID {req.UserId} not found.",
                StatusCodes.Status404NotFound);
        }

        var self = await _userService.GetUserFromJwtAsync(ct);
        if (!_userService.HasPermission(PermissionName.UpdateUser) && self.CompanyId != user.CompanyId)
        {
            ThrowError(
                x => x.UserId,
                $"Not authorized to update user {req.UserId} outside of company",
                StatusCodes.Status403Forbidden);
        }

        var accessToken = await _authService.GetAccessTokenAsync(ct);

        // look up the desired Auth0 role ID
        var desiredAuth0RoleId = await _authService.GetRoleIdForRoleNameAsync(accessToken, req.Role!.Value.ToString(), ct);
        if (desiredAuth0RoleId is null)
        {
            _logger.LogWarning("Requested non-existent role: {requestedRole}.", req.Role);
            ThrowError("Requested role does not exist", StatusCodes.Status400BadRequest);
        }

        // remove existing roles for user with validation that they are valid roles
        var auth0Roles = await _authService.GetUsersRolesAsync(accessToken, user.ProviderId, ct);
        foreach (var auth0Role in auth0Roles)
        {
            if (!Enum.TryParse(auth0Role.Name, ignoreCase: true, out UserRole _))
            {
                throw new InvalidOperationException($"Auth0 Role not found in {nameof(UserRole)} enum: \"{auth0Role.Name}\".");
            }
        }

        if (auth0Roles.Count > 1)
        {
            _logger.LogError("User {userId} unexpectedly has multiple roles: {roles}. Removing all of them.", user.Id, auth0Roles);
        }

        _logger.LogInformation("Removing roles {roles} from user {userId}.", auth0Roles, user.Id);
        await _authService.RemoveRolesFromUserAsync(accessToken, user.ProviderId, auth0Roles.Select(r => r.Id).ToList(), ct);

        // update the user's role
        _logger.LogInformation("Adding role {roleId} to user {userId}.", desiredAuth0RoleId, user.Id);
        await _authService.AssignRoleToUserAsync(accessToken, user.ProviderId, desiredAuth0RoleId, CancellationToken.None);
        user.Role = req.Role!.Value;
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        await SendNoContentAsync(ct);
    }
}