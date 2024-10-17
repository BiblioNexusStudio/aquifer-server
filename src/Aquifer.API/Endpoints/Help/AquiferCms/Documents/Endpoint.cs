using Aquifer.API.Helpers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Help.AquiferCms.Documents;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/help/aquifer-cms/documents");
        ResponseCache(EndpointHelpers.OneHourInSeconds);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var helpDocuments = await GetHelpDocumentsAsync(ct);
        var response = new Response
        {
            Releases = helpDocuments.Where(x => x.Type == HelpDocumentType.Release),
            HowTos = helpDocuments
                .Where(x => x.Type == HelpDocumentType.HowTo)
                .OrderBy(x => x.Title)
        };
        await SendOkAsync(response, ct);
    }

    private async Task<List<HelpDocumentResponse>> GetHelpDocumentsAsync(CancellationToken ct)
    {
        return await dbContext
            .HelpDocuments
            .Where(x => x.Enabled)
            .OrderByDescending(x => x.Created)
            .Select(x => new HelpDocumentResponse
            {
                Title = x.Title,
                Type = x.Type,
                Url = x.Url,
                ThumbnailUrl = x.ThumbnailUrl
            })
            .ToListAsync(ct);
    }
}