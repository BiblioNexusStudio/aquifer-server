using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.ParentResources.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/parent-resources");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.ParentResources
            .Where(pr => (req.LanguageId == null || pr.Resources.Any(r => r.ResourceContents.Any(rc => rc.LanguageId == req.LanguageId))) &&
                         (req.ResourceType == null || pr.ResourceType == req.ResourceType))
            .Select(pr => new Response
            {
                ComplexityLevel = pr.ComplexityLevel,
                DisplayName = pr.DisplayName,
                LicenseInfoValue = pr.LicenseInfo,
                ResourceType = pr.ResourceType,
                ShortName = pr.ShortName
            })
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}