using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.ParentResources.List;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/parent-resources");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var parentResources = await _dbContext.ParentResources
            .Where(pr => pr.ResourceType == req.ResourceType)
            .ToListAsync(ct);

        await SendOkAsync(new Response { ParentResources = parentResources }, ct);
    }
}