using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.GetSelf;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/users/self");
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
            Company = new CompanyResponse { Id = user.CompanyId },
            LanguageId = user.LanguageId,
        };

        if (userService.HasPermission(PermissionName.CreateCommunityContent)) {
            response.HasAssignedContent = await dbContext.ResourceContentVersions
                .AnyAsync(x => x.AssignedUserId == user.Id, ct);
        }

        await SendOkAsync(response, ct);
    }
}