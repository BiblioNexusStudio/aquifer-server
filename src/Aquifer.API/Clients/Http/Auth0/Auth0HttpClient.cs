using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.Common.Clients;
using Aquifer.Common.Utilities;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Clients.Http.Auth0;

public interface IAuth0HttpClient
{
    Task<HttpResponseMessage> CreateUserAsync(string name, string email, string authToken, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAuth0TokenAsync(CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetUserRolesAsync(string auth0Token, CancellationToken cancellationToken);
    Task<HttpResponseMessage> AssignUserToRoleAsync(string roleId, string userId, string auth0Token, CancellationToken cancellationToken);
    Task<HttpResponseMessage> ResetPasswordAsync(string email, string authToken, CancellationToken cancellationToken);
    Task<HttpResponseMessage> BlockUserAsync(string auth0Id, string authToken, CancellationToken cancellationToken);
}

public class Auth0HttpClient : IAuth0HttpClient
{
    private readonly Auth0Settings _authSettings;
    private readonly HttpClient _httpClient;
    private readonly IAzureKeyVaultClient _keyVaultClient;

    public Auth0HttpClient(HttpClient httpClient,
        IOptions<ConfigurationOptions> options,
        IAzureKeyVaultClient keyVaultClient)
    {
        _keyVaultClient = keyVaultClient;
        _authSettings = options.Value.Auth0Settings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_authSettings.BaseUri);
    }

    public async Task<HttpResponseMessage> GetUserRolesAsync(string auth0Token, CancellationToken cancellationToken)
    {
        SetAuthHeader(auth0Token);

        return await _httpClient.GetAsync("/api/v2/roles", cancellationToken);
    }

    public async Task<HttpResponseMessage> GetAuth0TokenAsync(CancellationToken cancellationToken)
    {
        var tokenRequest = new Auth0TokenRequest
        {
            ClientId = _authSettings.ApiClientId,
            ClientSecret = await _keyVaultClient.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret),
            Audience = _authSettings.Audience,
            GrantType = Auth0Constants.ClientCredentials
        };

        var request = GetRequestAsStringContent(tokenRequest);

        return await _httpClient.PostAsync("/oauth/token", request, cancellationToken);
    }

    public async Task<HttpResponseMessage> AssignUserToRoleAsync(string roleId,
        string userId,
        string auth0Token,
        CancellationToken cancellationToken)
    {
        SetAuthHeader(auth0Token);

        var postUri = $"/api/v2/roles/{roleId}/users";

        var requestBody = new Auth0AssignUserRolesRequest { Users = [userId] };
        var request = GetRequestAsStringContent(requestBody);

        return await _httpClient.PostAsync(postUri, request, cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateUserAsync(string name, string email, string authToken, CancellationToken cancellationToken)
    {
        SetAuthHeader(authToken);

        var createUserRequest = new Auth0CreateUserRequest
        {
            Connection = Auth0Constants.Connection,
            Email = email,
            Password = CryptographyUtilities.GenerateSimplePassword(),
            Name = name
        };

        var request = GetRequestAsStringContent(createUserRequest);

        return await _httpClient.PostAsync("/api/v2/users", request, cancellationToken);
    }

    public async Task<HttpResponseMessage> ResetPasswordAsync(string email, string authToken, CancellationToken cancellationToken)
    {
        const string endpointPath = "/dbconnections/change_password";
        SetAuthHeader(authToken);

        var requestModel = new Auth0PasswordResetRequest
        {
            Email = email,
            ClientId = _authSettings.ApplicationClientId
        };

        var request = GetRequestAsStringContent(requestModel);
        return await _httpClient.PostAsync(endpointPath, request, cancellationToken);
    }

    public async Task<HttpResponseMessage> BlockUserAsync(string auth0Id, string authToken, CancellationToken cancellationToken)
    {
        var endpointPath = $"/api/v2/users/{auth0Id}";
        SetAuthHeader(authToken);
        Auth0BlockUserRequest requestModel = new();
        var request = GetRequestAsStringContent(requestModel);

        return await _httpClient.PatchAsync(endpointPath, request, cancellationToken);
    }

    private void SetAuthHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private StringContent GetRequestAsStringContent(object content)
    {
        return new StringContent(JsonUtilities.DefaultSerialize(content), Encoding.UTF8, MediaTypeNames.Application.Json);
    }
}