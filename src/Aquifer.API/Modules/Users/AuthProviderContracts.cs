using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Users;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
}

public class CreateOAuthUserRequest
{
    public string Connection { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class CreateUserResponse
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = null!;
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

public class GetRolesResponse
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class PostRoleUsersRequest
{
    public List<string> Users { get; set; } = null!;
}