using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.Disable;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    IAuth0HttpClient auth0HttpClient,
    ILogger<Endpoint> logger) : Endpoint<Request>
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

        var token = await GetAuth0TokenAsync(ct);
        await BlockAuth0UserAsync(userToDisable, token, ct);

        userToDisable.Enabled = false;
        await dbContext.SaveChangesAsync(ct);
    }

    private async Task BlockAuth0UserAsync(UserEntity userToDisable, string token, CancellationToken ct)
    {
        var blockUserResponse = await auth0HttpClient.BlockUser(userToDisable.ProviderId, token, ct);
        var blockUserResponseContent = await blockUserResponse.Content.ReadAsStringAsync(ct);
        if (!blockUserResponse.IsSuccessStatusCode)
        {
            HandleErrorAsync(blockUserResponse, blockUserResponseContent, "Error blocking user on Auth0");
        }
    }

    private async Task<string> GetAuth0TokenAsync(CancellationToken ct)
    {
        var authTokenResponse = await auth0HttpClient.GetAuth0Token(ct);
        var authTokenResponseContent = await authTokenResponse.Content.ReadAsStringAsync(ct);
        if (!authTokenResponse.IsSuccessStatusCode)
        {
            HandleErrorAsync(authTokenResponse, authTokenResponseContent, "Error getting Auth0 access token");
        }

        return JsonUtilities.DefaultDeserialize<Auth0TokenResponse>(authTokenResponseContent).AccessToken;
    }

    private void HandleErrorAsync(HttpResponseMessage responseMessage, string responseMessageContent, string error)
    {
        logger.LogError("{error} - {statusCode} - {responseMessageContent}",
            error,
            (int)responseMessage.StatusCode,
            responseMessageContent);

        ThrowError("Error from Auth0");
    }
}