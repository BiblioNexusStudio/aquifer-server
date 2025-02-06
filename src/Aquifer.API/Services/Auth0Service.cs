using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.Common.Clients;
using Aquifer.Common.Utilities;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;

namespace Aquifer.API.Services;

public interface IAuth0Service
{
    Task<string> GetAccessTokenAsync(CancellationToken ct);

    Task<IReadOnlyList<(string Id, string Name)>> GetAllRolesAsync(string accessToken, CancellationToken ct);
    Task<string?> GetRoleIdForRoleNameAsync(string accessToken, string roleName, CancellationToken ct);

    Task<IReadOnlyList<(string Id, string Name)>> GetUsersRolesAsync(
        string accessToken,
        string auth0UserId,
        CancellationToken ct);
    Task AssignRoleToUserAsync(string accessToken, string auth0UserId, string roleId, CancellationToken ct);
    Task RemoveRolesFromUserAsync(string accessToken, string auth0UserId, IReadOnlyList<string> roleIds, CancellationToken ct);

    Task<string> CreateUserAsync(string accessToken, string email, string fullName, CancellationToken ct);
    Task BlockUserAsync(string accessToken, string auth0UserId, CancellationToken ct);

    Task ResetPasswordAsync(string accessToken, string email, CancellationToken ct);
}

public sealed class Auth0Service : IAuth0Service
{
    private readonly Auth0Settings _auth0Options;
    private readonly IAzureKeyVaultClient _keyVaultClient;

    private const string Connection = "Username-Password-Authentication";

    public Auth0Service(Auth0Settings auth0Options, IAzureKeyVaultClient keyVaultClient)
    {
        _auth0Options = auth0Options;
        _keyVaultClient = keyVaultClient;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        using var auth0AuthenticationClient = GetAuthenticationApiClient();

        var request = new ClientCredentialsTokenRequest
        {
            ClientId = _auth0Options.ApiClientId,
            ClientSecret = await _keyVaultClient.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret),
            Audience = _auth0Options.Audience,
            //GrantType = Auth0Constants.ClientCredentials, ????
        };

        var tokenResponse = await auth0AuthenticationClient.GetTokenAsync(request, ct);

        return tokenResponse.AccessToken;
    }

    public async Task<IReadOnlyList<(string Id, string Name)>> GetAllRolesAsync(string accessToken, CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        var getRolesResponse = await auth0ManagementClient.Roles.GetAllAsync(
            new GetRolesRequest(),
            new PaginationInfo(pageNo: 0, perPage: 50), // one page should be enough to fetch all of our roles
            ct);

        return getRolesResponse
            .Select(r => (r.Id, r.Name))
            .ToList();
    }

    public async Task<string?> GetRoleIdForRoleNameAsync(string accessToken, string roleName, CancellationToken ct)
    {
        var roles = await GetAllRolesAsync(accessToken, ct);

        return roles
            .Where(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
            .Select(r => r.Id)
            .FirstOrDefault();
    }

    public async Task<IReadOnlyList<(string Id, string Name)>> GetUsersRolesAsync(
        string accessToken,
        string auth0UserId,
        CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        var getUserRolesResponse = await auth0ManagementClient.Users.GetRolesAsync(
            auth0UserId,
            new PaginationInfo(pageNo: 0, perPage: 50), // one page should be enough to fetch all of our roles
            cancellationToken: ct);

        return getUserRolesResponse
            .Select(r => (r.Id, r.Name))
            .ToList();
    }

    public async Task AssignRoleToUserAsync(string accessToken, string auth0UserId, string roleId, CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        await auth0ManagementClient.Users.AssignRolesAsync(auth0UserId, new AssignRolesRequest { Roles = [roleId] }, ct);
    }

    public async Task RemoveRolesFromUserAsync(string accessToken, string auth0UserId, IReadOnlyList<string> roleIds, CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        await auth0ManagementClient.Users.RemoveRolesAsync(auth0UserId, new AssignRolesRequest { Roles = roleIds.ToArray() }, ct);
    }

    public async Task<string> CreateUserAsync(string accessToken, string email, string fullName, CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        var createUserResponse = await auth0ManagementClient.Users.CreateAsync(
            new UserCreateRequest
            {
                Connection = Connection,
                Email = email,
                Password = CryptographyUtilities.GenerateSimplePassword(),
                FullName = fullName,
            },
            ct);

        return createUserResponse.UserId;
    }

    public async Task BlockUserAsync(string accessToken, string auth0UserId, CancellationToken ct)
    {
        using var auth0ManagementClient = GetManagementApiClient(accessToken);

        await auth0ManagementClient.Users.UpdateAsync(auth0UserId, new UserUpdateRequest { Blocked = true }, ct);
    }

    public async Task ResetPasswordAsync(string accessToken, string email, CancellationToken ct)
    {
        using var auth0AuthenticationClient = GetAuthenticationApiClient();

        await auth0AuthenticationClient.ChangePasswordAsync(
            new ChangePasswordRequest
            {
                Email = email,
                ClientId = _auth0Options.ApplicationClientId,
                Connection = Connection,
            },
            ct);
    }

    private AuthenticationApiClient GetAuthenticationApiClient()
    {
        return new AuthenticationApiClient(new Uri(_auth0Options.BaseUri));
    }

    private ManagementApiClient GetManagementApiClient(string accessToken)
    {
        return new ManagementApiClient(accessToken, new Uri($"{_auth0Options.BaseUri}/api/v2"));
    }
}