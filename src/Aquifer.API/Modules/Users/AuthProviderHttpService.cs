using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.API.Services;
using Aquifer.API.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Aquifer.API.Modules.Users;

public interface IAuthProviderHttpService
{
    Task<HttpResponseMessage> CreateUser(CreateUserRequest userRequest, string authToken,
        CancellationToken cancellationToken);

    Task<HttpResponseMessage> GetAuth0Token(CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetUserRoles(string auth0Token, CancellationToken cancellationToken);

    Task<HttpResponseMessage> AssignUserToRole(string roleId,
        string userId,
        string auth0Token,
        CancellationToken cancellationToken);
}

public class AuthProviderHttpService : IAuthProviderHttpService
{
    private readonly Auth0Settings _authSettings;
    private readonly HttpClient _httpClient;
    private readonly IAzureKeyVaultService _keyVaultService;

    public AuthProviderHttpService(HttpClient httpClient,
        IOptions<ConfigurationOptions> options,
        IAzureKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
        _authSettings = options.Value.Auth0Settings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_authSettings.BaseUri);
    }

    public async Task<HttpResponseMessage> CreateUser(CreateUserRequest user, string authToken,
        CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authToken);

        //_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {authToken}");

        var createUserRequest = new CreateOAuthUserRequest
        {
            Connection = Auth0Constants.Connection,
            Email = user.Email,
            Password = user.Password,
            Name = $"{user.FirstName} {user.LastName}"
        };
        string createUserJsonContent = JsonUtilities.DefaultSerialize(createUserRequest);
        var createUserHttpContent =
            new StringContent(createUserJsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        return await _httpClient.PostAsync("/api/v2/users", createUserHttpContent, cancellationToken);
    }

    public async Task<HttpResponseMessage> GetUserRoles(string auth0Token, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth0Token);

        return await _httpClient.GetAsync("/api/v2/roles", cancellationToken);
    }

    public async Task<HttpResponseMessage> GetAuth0Token(CancellationToken cancellationToken)
    {
        var tokenRequest = new Auth0TokenRequest
        {
            ClientId = _authSettings.ClientId,
            ClientSecret = await _keyVaultService.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret),
            Audience = _authSettings.Audience,
            GrantType = Auth0Constants.ClientCredentials
        };

        string jsonContent = JsonUtilities.DefaultSerialize(tokenRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        return await _httpClient.PostAsync("/oauth/token", httpContent, cancellationToken);
    }

    public async Task<HttpResponseMessage> AssignUserToRole(string roleId,
        string userId,
        string auth0Token,
        CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth0Token);

        string postUri = $"/api/v2/roles/{roleId}/users";

        var requestBody = new PostRoleUsersRequest { Users = [userId] };
        string jsonContent = JsonUtilities.DefaultSerialize(requestBody);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        return await _httpClient.PostAsync(postUri, httpContent, cancellationToken);
    }
}