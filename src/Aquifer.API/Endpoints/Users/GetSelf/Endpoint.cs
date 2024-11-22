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
        var user = await userService.GetUserWithCompanyLanguagesFromJwtAsync(ct);
        var permissions = userService.GetAllJwtPermissions();

        var response = new Response
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Permissions = permissions,
            Company = new CompanyResponse
            {
                Id = user.CompanyId,
                LanguageIds = user.Company.CompanyLanguages.Select(cl => cl.LanguageId).ToList()
            },
            LanguageId = user.LanguageId,
            CanBeAssignedContent = true
        };

        if (userService.HasPermission(PermissionName.CreateCommunityContent) && !userService.HasPermission(PermissionName.CreateContent))
        {
            var isAssignedContent = await dbContext.ResourceContentVersions.AnyAsync(x => x.AssignedUserId == user.Id, ct);

            if (isAssignedContent)
            {
                response.CanBeAssignedContent = false;
            }
        }

        await SendOkAsync(response, ct);
    }
}