using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Help.Aquifer_cms.Documents;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/help/aquifer-cms/documents");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var helpDocuments = await GetHelpDocumentsAsync(ct);
        var response = new Response
        {
            Releases = helpDocuments.FindAll(x => x.Type == HelpDocumentType.Release),
            HowTos = helpDocuments.FindAll(x => x.Type == HelpDocumentType.HowTo)
        };
        await SendOkAsync(response, ct);
    }

    private async Task<List<HelpDocumentResponse>> GetHelpDocumentsAsync(CancellationToken ct)
    {
        return await dbContext
            .HelpDocuments
            .OrderByDescending(x => x.Updated)
            .Select(x => new HelpDocumentResponse
            {
                Id = x.Id,
                Title = x.Title,
                Type = x.Type,
                Url = x.Url,
            })
            .ToListAsync(ct);
    }
}