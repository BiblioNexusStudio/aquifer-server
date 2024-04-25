using Aquifer.API.Helpers;
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
        Options(EndpointHelpers.SetCacheOption());
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.ParentResources
            .Where(pr => (req.LanguageId == 1 || pr.Localizations.Any(r => r.LanguageId == req.LanguageId)) &&
                (req.ResourceType == null || pr.ResourceType == req.ResourceType))
            .OrderBy(x => x.DisplayName)
            .Select(pr => new Response
            {
                ComplexityLevel = pr.ComplexityLevel,
                DisplayName =
                    req.LanguageId == 1
                        ? pr.DisplayName
                        : pr.Localizations.Single(l => l.LanguageId == req.LanguageId).DisplayName,
                LicenseInfoValue = pr.LicenseInfo,
                ResourceType = pr.ResourceType,
                ShortName = pr.ShortName,
                Id = pr.Id
            })
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}