using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/bibles");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bibles = await dbContext.Bibles.Where(b => b.Enabled && b.RestrictedLicense == request.RestrictedLicense)
            .Select(bible => new Response
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                Id = bible.Id,
                SerializedLicenseInfo = bible.LicenseInfo,
                LanguageId = bible.LanguageId,
                IsLanguageDefault = bible.LanguageDefault,
                RestrictedLicense = bible.RestrictedLicense,
                GreekAlignment = bible.GreekAlignment
            })
            .ToListAsync(ct);

        await SendOkAsync(bibles, ct);
    }
}