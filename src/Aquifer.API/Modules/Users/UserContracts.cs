using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Users;

public class UserResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class CurrentUserResponse : UserResponse
{
    public required IEnumerable<string> Permissions { get; set; }
}

public class UserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
}

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
}

public class CreateUserRequest
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
    public string id { get; set; } = null!;
    public string name { get; set; } = null!;
}

public class PostRoleUsersRequest
{
    public List<string> users { get; set; }
}