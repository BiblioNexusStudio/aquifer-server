using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;

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
        var userToDisable = dbContext.Users.SingleOrDefault(x => x.Id == req.UserId && x.Enabled);
        if (userToDisable is null ||
            (!currentUserPermissions.Contains(PermissionName.DisableUser) && currentUser.CompanyId != userToDisable.CompanyId))
        {
            await SendNotFoundAsync(ct);
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
        var response = await auth0HttpClient.BlockUser(userToDisable.ProviderId, token, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorAsync(response, responseContent, "Error getting Auth0 access token", ct);
            return;
        }

        await SendOkAsync(ct);
    }

    private async Task HandleErrorAsync(HttpResponseMessage response, string responseContent, string error, CancellationToken ct)
    {
        logger.LogError("{error} - {statusCode} - {responseContent}", error, (int)response.StatusCode, responseContent);
        await SendStringAsync("An error occurred. Please try again later.", 500, cancellation: ct);
    }
}