using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/bibles");
        Options(EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var bibles = await dbContext.Bibles.Where(b => b.Enabled)
            .Select(bible => new Response
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                Id = bible.Id,
                SerializedLicenseInfo = bible.LicenseInfo,
                LanguageId = bible.LanguageId,
                IsLanguageDefault = bible.LanguageDefault
            })
            .ToListAsync(ct);

        await SendOkAsync(bibles, ct);
    }
}
