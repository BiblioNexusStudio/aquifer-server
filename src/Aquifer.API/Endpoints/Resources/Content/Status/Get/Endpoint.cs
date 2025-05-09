using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Status.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/{ContentId}/status");
        Permissions(PermissionName.ReadResources);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var result = await dbContext.ResourceContents
            .Where(rc => rc.Id == request.ContentId)
            .Select(rc => new Response { Status = rc.Status })
            .SingleOrDefaultAsync(ct);

        if (result is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = result;
    }
}