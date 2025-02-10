using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.Disable;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    IAuth0Service auth0Service)
    : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/users/{UserId}/disable");
        Permissions(PermissionName.DisableUser, PermissionName.DisableUsersInCompany);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var currentUser = await userService.GetUserFromJwtAsync(ct);
        var currentUserPermissions = userService.GetAllJwtPermissions();
        var userToDisable = dbContext.Users
            .AsTracking()
            .SingleOrDefault(x => x.Id == req.UserId && x.Enabled);
        if (userToDisable is null ||
            (!currentUserPermissions.Contains(PermissionName.DisableUser) && currentUser.CompanyId != userToDisable.CompanyId))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (userToDisable.Role is UserRole.Publisher or UserRole.Admin && currentUser.Role != UserRole.Admin)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var token = await auth0Service.GetAccessTokenAsync(ct);
        await auth0Service.BlockUserAsync(token, userToDisable.ProviderId, ct);

        userToDisable.Enabled = false;
        await dbContext.SaveChangesAsync(ct);
    }
}