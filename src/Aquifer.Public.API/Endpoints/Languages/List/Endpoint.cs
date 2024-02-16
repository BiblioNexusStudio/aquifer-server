﻿using Aquifer.Data;
using Aquifer.Public.API.Helpers;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Languages.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/languages");
        Options(EndpointHelpers.SetCacheOption(60));
        Summary(s =>
        {
            s.Summary = "Return language list";
            s.Description = "Return a list of languages that can have associated resources in the Aquifer.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var languages = await dbContext.Languages.Select(x => new Response
        {
            Id = x.Id,
            Code = x.ISO6393Code,
            EnglishDisplay = x.EnglishDisplay,
            LocalizedDisplay = x.DisplayName,
            ScriptDirection = x.ScriptDirection.ToString()
        }).ToListAsync(ct);

        await SendAsync(languages, 200, ct);
    }
}