using Aquifer.API.Clients.Http.Auth0;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Users.RolesToPermissions;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    ILogger<Endpoint> logger,
    IAuth0HttpClient authProviderService
) : EndpointWithoutRequest<Dictionary<string, string[]>>
{
    public override void Configure()
    {
        Get("/users/rolesToPermissions");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var accessToken = await GetAccessTokenAsync(ct);

        var response = await authProviderService.GetUserRoles(accessToken, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Error getting Auth0 roles: {statusCode} - {response}", response.StatusCode, responseContent);
            ThrowError(responseContent, (int)response.StatusCode);
        }

        var roles = JsonUtilities.DefaultDeserialize<List<Auth0AssignUserRolesResponse>>(responseContent);
        var rolesToPermissions = new Dictionary<string, string[]>();

        foreach (var role in roles)
        {
            var permissionResponse = await authProviderService.GetPermissionsForRole(accessToken, ct, role.Id);
            var permissionResponseContent = await permissionResponse.Content.ReadAsStringAsync(ct);
            if (!permissionResponse.IsSuccessStatusCode)
            {
                logger.LogWarning("Error getting Auth0 permissions: {statusCode} - {res}", permissionResponse.StatusCode, permissionResponseContent);
                ThrowError(permissionResponseContent, (int)permissionResponse.StatusCode);
            }
            var rolePermissions = JsonUtilities.DefaultDeserialize<List<Auth0PermissionsForRolesResponse>>(permissionResponseContent);
            rolesToPermissions[role.Name] = rolePermissions.Select(p => p.PermissionName).ToArray();
        }
        await SendOkAsync(rolesToPermissions, ct);
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        var response = await authProviderService.GetAuth0Token(ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Error getting Auth0 token: {statusCode} - {response}", response.StatusCode, responseContent);
            ThrowError(responseContent, (int)response.StatusCode);
        }

        return JsonUtilities.DefaultDeserialize<Auth0TokenResponse>(responseContent).AccessToken;
    }
}