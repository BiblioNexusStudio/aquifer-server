using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Help;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/help");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new Response
        {
            Releases = await GetReleasesAsync(ct),
            HowTos = await GetHowTosAsync(ct)
        };
        await SendOkAsync(response, ct);
    }

    private async Task<List<HelpDocumentEntity>> GetReleasesAsync(CancellationToken ct)
    {
        return await dbContext
            .HelpDocuments
            .Where(x => x.Type == HelpDocumentType.Release)
            .OrderByDescending(x => x.Updated)
            .Take(10)
            .ToListAsync(ct);
    }

    private async Task<List<HelpDocumentEntity>> GetHowTosAsync(CancellationToken ct)
    {
        return await dbContext
            .HelpDocuments
            .Where(x => x.Type == HelpDocumentType.HowTo)
            .OrderByDescending(x => x.Updated)
            .ToListAsync(ct);
    }
}