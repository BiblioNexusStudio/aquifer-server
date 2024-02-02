using Aquifer.API.Clients.Http.Auth0;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Users.ResetPassword;

public class Endpoint(IAuth0HttpClient auth0Client) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/users/password-reset");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await auth0Client.GetAuth0Token(ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);


        await SendAsync(responseContent, 200, ct);
    }
}