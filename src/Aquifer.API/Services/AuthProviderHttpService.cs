using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.API.Modules.Users;
using Aquifer.API.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Aquifer.API.Services;

public interface IAuthProviderHttpService
{
    Task<string> CreateUser(UserRequest userRequest, CancellationToken cancellationToken);
}

public class AuthProviderHttpService : IAuthProviderHttpService
{
    private readonly Auth0Settings _authSettings;
    private readonly HttpClient _httpClient;
    private readonly IAzureKeyVaultService _keyVaultService;

    public AuthProviderHttpService(HttpClient httpClient, IOptions<ConfigurationOptions> options,
        IAzureKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
        _authSettings = options.Value.Auth0Settings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_authSettings.BaseUri);
    }

    public async Task<string> CreateUser(UserRequest user, CancellationToken cancellationToken)
    {
        string auth0Token = await GetAuth0Token(cancellationToken);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", auth0Token);

        var roles = await GetUserRoles(auth0Token, cancellationToken);

        var role = roles.FirstOrDefault(r => r.name == user.Role);

        var createUserRequest = new CreateUserRequest
        {
            Connection = Auth0Constants.Connection,
            Email = user.Email,
            Password = user.Password,
            Name = $"{user.FirstName} {user.LastName}"
        };
        string createUserJsonContent = JsonUtilities.DefaultSerialize(createUserRequest);
        var createUserHttpContent =
            new StringContent(createUserJsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        var createUserResponse = await _httpClient.PostAsync("/api/v2/users", createUserHttpContent, cancellationToken);
        string createUserResponseContent = await createUserResponse.Content.ReadAsStringAsync(cancellationToken);
        var createUserResponseObject = JsonUtilities.DefaultDeserialize<CreateUserResponse>(createUserResponseContent);

        await AssignUserToRole(role, createUserResponseObject.UserId, auth0Token,
            cancellationToken);

        return createUserResponseObject.UserId;
    }

    private async Task<List<GetRolesResponse>> GetUserRoles(string auth0Token, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth0Token);

        var response = await _httpClient.GetAsync("/api/v2/roles", cancellationToken);

        string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var responseObject = JsonUtilities.DefaultDeserialize<List<GetRolesResponse>>(responseContent);

        return responseObject;
    }

    private async Task AssignUserToRole(GetRolesResponse role, string userId, string auth0Token,
        CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth0Token);

        string postUri = $"/api/v2/roles/{role.id}/users";

        var requestBody = new PostRoleUsersRequest { users = { userId } };

        await _httpClient.PostAsync(postUri,
            new StringContent(JsonUtilities.DefaultSerialize(requestBody), Encoding.UTF8,
                MediaTypeNames.Application.Json), cancellationToken);
    }

    private async Task<string> GetAuth0Token(CancellationToken cancellationToken)
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

        var response = await _httpClient.PostAsync("/oauth/token", httpContent, cancellationToken);

        string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var responseObject = JsonUtilities.DefaultDeserialize<TokenResponse>(responseContent);

        return responseObject.AccessToken;
    }
}