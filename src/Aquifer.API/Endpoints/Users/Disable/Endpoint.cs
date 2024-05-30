using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.Disable;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    IAuth0HttpClient auth0HttpClient,
    ILogger<Endpoint> logger) : Endpoint<Request, Response>
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
        var userToDisable = dbContext.Users.SingleOrDefault(x => x.Id == req.UserId && x.Enabled);
        if (userToDisable is null ||
            (!currentUserPermissions.Contains(PermissionName.DisableUser) && currentUser.CompanyId != userToDisable.CompanyId))
        {
            Response response = new()
            {
                HasError = true,
                Error = "User not found"
            };

            await SendAsync(response, 404, ct);
            return;
        }

        var assignedResources = await dbContext.ResourceContentVersions.Where(x => x.AssignedUserId == userToDisable.Id).ToListAsync(ct);
        if (assignedResources.Count > 0)
        {
            Response response = new()
            {
                HasError = true,
                Error = "Cannot remove a user with assigned items",
                AssignedResources = assignedResources.Select(x => new DisableUserAssignedResourcesResponse
                {
                    ResourceContentId = x.ResourceContentId,
                    DisplayName = x.DisplayName
                })
            };

            await SendAsync(response, 400, ct);
            return;
        }

        var authTokenResponse = await auth0HttpClient.GetAuth0Token(ct);
        var authTokenResponseContent = await authTokenResponse.Content.ReadAsStringAsync(ct);
        if (!authTokenResponse.IsSuccessStatusCode)
        {
            await HandleErrorAsync(authTokenResponse, authTokenResponseContent, "Error getting Auth0 access token", ct);
            return;
        }

        var token = JsonUtilities.DefaultDeserialize<Auth0TokenResponse>(authTokenResponseContent).AccessToken;
        var blockUserResponse = await auth0HttpClient.BlockUser(userToDisable.ProviderId, token, ct);
        var blockUserResponseContent = await blockUserResponse.Content.ReadAsStringAsync(ct);
        if (!blockUserResponse.IsSuccessStatusCode)
        {
            await HandleErrorAsync(blockUserResponse, blockUserResponseContent, "Error blocking user on Auth0", ct);
            return;
        }

        userToDisable.Enabled = false;
        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new Response(), ct);
    }

    private async Task HandleErrorAsync(HttpResponseMessage responseMessage,
        string responseMessageContent,
        string error,
        CancellationToken ct)
    {
        logger.LogError("{error} - {statusCode} - {responseMessageContent}",
            error,
            (int)responseMessage.StatusCode,
            responseMessageContent);

        Response response = new()
        {
            HasError = true,
            Error = "An error occurred. Please try again later."
        };

        await SendAsync(response, 500, ct);
    }
}