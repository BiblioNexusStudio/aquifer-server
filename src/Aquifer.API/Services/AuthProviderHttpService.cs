using Aquifer.API.Common;
using Aquifer.API.Configuration;
using Aquifer.API.Modules.Users;
using Aquifer.API.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;

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

    public AuthProviderHttpService(HttpClient httpClient, IOptions<ConfigurationOptions> options, IAzureKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
        _authSettings = options.Value.Auth0Settings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_authSettings.BaseUri);
    }

    public async Task<string> CreateUser(UserRequest user, CancellationToken cancellationToken)
    {
        var tokenRequest = new Auth0TokenRequest
        {
            ClientId = _authSettings.ClientId,
            ClientSecret = await _keyVaultService.GetSecretAsync(KeyVaultSecretName.Auth0ClientSecret),
            Audience = _authSettings.Audience,
            GrantType = "client_credentials"
        };

        string jsonContent = JsonUtilities.DefaultSerialize(tokenRequest);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/oauth/token", httpContent, cancellationToken);

        string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var responseObject = JsonUtilities.DefaultDeserialize<TokenResponse>(responseContent);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", responseObject.access_token);

        var createUserRequest = new
        {
            connection = "Username-Password-Authentication",
            email = user.Email,
            password = user.Password,
            name = $"{user.FirstName} {user.LastName}"
        };
        string createUserJsonContent = JsonUtilities.DefaultSerialize(createUserRequest);
        var createUserHttpContent = new StringContent(createUserJsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        var createUserResponse = await _httpClient.PostAsync("/api/v2/users", createUserHttpContent, cancellationToken);
        string createUserResponseContent = await createUserResponse.Content.ReadAsStringAsync(cancellationToken);
        var createUserResponseObject = JsonUtilities.DefaultDeserialize<CreateUserResponse>(createUserResponseContent);

        return createUserResponseObject.user_id;
    }
}

public class Auth0TokenRequest
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; init; } = null!;

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; init; } = null!;

    public string Audience { get; init; } = null!;

    [JsonPropertyName("grant_type")]
    public string GrantType { get; init; } = null!;
}