using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await GetResourceContentAsync(req, ct);
        await SendAsync(response, 200, ct);
    }

    private async Task<Response> GetResourceContentAsync(Request req, CancellationToken ct)
    {
        var response = await _dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == req.ContentId && x.IsPublished).Select(x => new Response
            {
                Id = x.ResourceContentId,
                Name = x.ResourceContent.Resource.EnglishLabel,
                LocalizedName = x.DisplayName,
                ContentValue = x.Content,
                Language = new ResourceContentLanguage
                {
                    Id = x.ResourceContent.Language.Id,
                    DisplayName = x.ResourceContent.Language.EnglishDisplay,
                    Code = x.ResourceContent.Language.ISO6393Code,
                    ScriptDirection = x.ResourceContent.Language.ScriptDirection
                },
                Grouping = new ResourceTypeMetadata
                {
                    Name = x.ResourceContent.Resource.ParentResource.DisplayName,
                    Type = x.ResourceContent.Resource.ParentResource.ResourceType,
                    LicenseInfoValue = x.ResourceContent.Resource.ParentResource.LicenseInfo
                }
            }).SingleOrDefaultAsync(ct);

        if (response is null)
        {
            ThrowError($"No record found for {req.ContentId}", 404);
        }

        return response;
    }
}