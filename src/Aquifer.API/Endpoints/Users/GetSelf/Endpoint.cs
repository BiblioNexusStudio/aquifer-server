using Aquifer.API.Services;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Users.GetSelf;

public class Endpoint(IUserService userService) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/users/self", "/admin/users/self");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var permissions = userService.GetAllJwtPermissions();

        var response = new Response
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Permissions = permissions,
            Company = new() { Id = user.CompanyId }
        };

        await SendAsync(response, 200, ct);
    }
}