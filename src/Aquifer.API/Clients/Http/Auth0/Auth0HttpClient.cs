using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Microsoft.Extensions.Options;

namespace Aquifer.API.Clients.Http.Auth0;

public interface IAuth0HttpClient
{
    Task<HttpResponseMessage> CreateUser(string name, string email, string authToken, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAuth0Token(CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetUserRoles(string auth0Token, CancellationToken cancellationToken);
    Task<HttpResponseMessage> AssignUserToRole(string roleId, string userId, string auth0Token, CancellationToken cancellationToken);
    Task<HttpResponseMessage> ResetPassword(string email, string authToken, CancellationToken cancellationToken);
}

public class Auth0HttpClient : IAuth0HttpClient
{
    private readonly Auth0Settings _authSettings;
    private readonly HttpClient _httpClient;
    private readonly IAzureKeyVaultService _keyVaultService;

    public Auth0HttpClient(HttpClient httpClient,
        IOptions<ConfigurationOptions> options,
        IAzureKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
        _authSettings = options.Value.Auth0Settings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_authSettings.BaseUri);
    }

    public async Task<HttpResponseMessage> GetUserRoles(string auth0Token, CancellationToken cancellationToken)
    {
        SetAuthHeader(auth0Token);

        return await _httpClient.GetAsync("/api/v2/roles", cancellationToken);
    }

    public async Task<HttpResponseMessage> GetAuth0Token(CancellationToken cancellationToken)
    {
        var tokenRequest = new Auth0TokenRequest
        {
            ClientId = _authSettings.ApiClientId,
            ClientSecret = await _keyVaultService.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret),
            Audience = _authSettings.Audience,
            GrantType = Auth0Constants.ClientCredentials
        };

        var jsonContent = JsonUtilities.DefaultSerialize(tokenRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        return await _httpClient.PostAsync("/oauth/token", httpContent, cancellationToken);
    }

    public async Task<HttpResponseMessage> AssignUserToRole(string roleId,
        string userId,
        string auth0Token,
        CancellationToken cancellationToken)
    {
        SetAuthHeader(auth0Token);

        var postUri = $"/api/v2/roles/{roleId}/users";

        var requestBody = new Auth0AssignUserRolesRequest { Users = [userId] };
        var jsonContent = JsonUtilities.DefaultSerialize(requestBody);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        return await _httpClient.PostAsync(postUri, httpContent, cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateUser(string name, string email, string authToken, CancellationToken cancellationToken)
    {
        SetAuthHeader(authToken);

        var createUserRequest = new Auth0CreateUserRequest
        {
            Connection = Auth0Constants.Connection,
            Email = email,
            Password = CryptographyUtilities.GenerateSimplePassword(),
            Name = name
        };

        var createUserJsonContent = JsonUtilities.DefaultSerialize(createUserRequest);
        var createUserHttpContent = new StringContent(createUserJsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        return await _httpClient.PostAsync("/api/v2/users", createUserHttpContent, cancellationToken);
    }

    public async Task<HttpResponseMessage> ResetPassword(string email, string authToken, CancellationToken cancellationToken)
    {
        const string endpointPath = "/dbconnections/change_password";
        SetAuthHeader(authToken);

        var requestModel = new Auth0PasswordResetRequest
        {
            Email = email,
            ClientId = _authSettings.ApplicationClientId
        };

        var request = new StringContent(JsonUtilities.DefaultSerialize(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
        return await _httpClient.PostAsync(endpointPath, request, cancellationToken);
    }

    private void SetAuthHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}