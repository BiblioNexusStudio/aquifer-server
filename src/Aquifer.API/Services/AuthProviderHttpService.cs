using Aquifer.API.Modules.Users;
using Aquifer.API.Utilities;
using System.Net.Http.Headers;
using System.Text;

namespace Aquifer.API.Services;

public interface IAuthProviderHttpService
{
    Task<string> CreateUser(UserRequest userRequest, CancellationToken cancellationToken);
}

public class AuthProviderHttpService : IAuthProviderHttpService
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://dev-bjm6e3tp0dti2618.us.auth0.com")
    };

    public async Task<string> CreateUser(UserRequest user, CancellationToken cancellationToken)
    {
        var requestContent = new
        {
            client_id = "",
            client_secret = "",
            audience = "https://dev-bjm6e3tp0dti2618.us.auth0.com/api/v2/",
            grant_type = "client_credentials"
        };

        string jsonContent = JsonUtilities.DefaultSerialize(requestContent);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/oauth/token", httpContent, cancellationToken);

        string responseContent = await response.Content.ReadAsStringAsync();
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
        var createUserHttpContent = new StringContent(createUserJsonContent, Encoding.UTF8, "application/json");
        var createUserResponse = await _httpClient.PostAsync("/api/v2/users", createUserHttpContent, cancellationToken);
        string createUserResponseContent = await createUserResponse.Content.ReadAsStringAsync(cancellationToken);
        var createUserResponseObject = JsonUtilities.DefaultDeserialize<CreateUserResponse>(createUserResponseContent);

        return createUserResponseObject.user_id;
    }
}