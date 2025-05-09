using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Versions.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/versions/{VersionId}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var version = await dbContext.ResourceContentVersions.SingleOrDefaultAsync(rcvs => rcvs.Id == request.VersionId, ct);

        if (version is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(
            new Response
            {
                Id = version.Id,
                DisplayName = version.DisplayName,
                WordCount = version.WordCount,
                Created = version.Created,
                IsPublished = version.IsPublished,
                Version = version.Version,
                ContentValue = version.Content,
            },
            ct);
    }
}