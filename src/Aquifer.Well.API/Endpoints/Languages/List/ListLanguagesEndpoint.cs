using Aquifer.Data;
using Aquifer.Well.API.Helpers;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Well.API.Endpoints.Languages.List;

public class ListLanguagesEndpoint(AquiferDbReadOnlyContext dbContext) : EndpointWithoutRequest<IReadOnlyList<ListLanguagesResponse>>
{
    public override void Configure()
    {
        Get("/languages");
        Options(EndpointHelpers.ServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        Summary(s =>
        {
            s.Summary = "Return language list.";
            s.Description = "Return a list of languages that can have associated resources in the Aquifer.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var languages = await dbContext.Languages
            .Select(x => new ListLanguagesResponse
            {
                Id = x.Id,
                Code = x.ISO6393Code,
                EnglishDisplay = x.EnglishDisplay,
                LocalizedDisplay = x.DisplayName,
                ScriptDirection = x.ScriptDirection.ToString()
            })
            .ToListAsync(ct);

        await SendOkAsync(languages, ct);
    }
}