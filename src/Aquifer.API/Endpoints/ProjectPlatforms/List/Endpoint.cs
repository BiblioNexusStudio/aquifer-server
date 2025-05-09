using Aquifer.API.Common;
using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.ProjectPlatforms.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/project-platforms");
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projectPlatforms = await dbContext.ProjectPlatforms
            .Select(projectPlatform => new Response
            {
                Id = projectPlatform.Id,
                Name = projectPlatform.Name,
            })
            .ToListAsync(ct);

        await SendOkAsync(projectPlatforms, ct);
    }
}