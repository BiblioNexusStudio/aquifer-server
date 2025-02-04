using Aquifer.API.Helpers;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Languages.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/languages");
        ResponseCache(EndpointHelpers.TenMinutesInSeconds);
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.TenMinutesInSeconds));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var languages = await dbContext.Languages.Select(x => new Response
            {
                Id = x.Id,
                Iso6393Code = x.ISO6393Code,
                EnglishDisplay = x.EnglishDisplay,
                DisplayName = x.DisplayName,
                Enabled = x.Enabled,
                ScriptDirection = x.ScriptDirection
            })
            .OrderBy(x => x.Iso6393Code)
            .ToListAsync(ct);

        await SendOkAsync(languages, ct);
    }
}