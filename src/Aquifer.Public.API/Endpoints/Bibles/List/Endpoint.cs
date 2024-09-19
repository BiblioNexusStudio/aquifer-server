﻿using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IReadOnlyList<Response>>
{
    private readonly AquiferDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/bibles");
        Description(d => d.ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Get a list of Bibles.";
            s.Description =
                "For a given optional language ID, returns the Bibles in the system including language information, abbreviation codes, whether or not the Bible supports a Greek alignment, and license information.";
        });
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bibles = await _dbContext.Bibles
            .Where(b =>
                b.Enabled &&
                !b.RestrictedLicense &&
                (!request.LanguageId.HasValue || b.LanguageId == request.LanguageId) &&
                (request.LanguageCode == null || b.Language.ISO6393Code == request.LanguageCode) &&
                (!request.IsLanguageDefault.HasValue || b.LanguageDefault == request.IsLanguageDefault) &&
                (!request.HasAudio.HasValue || b.BibleBookContents.Any(bbc => bbc.AudioUrls != null) == request.HasAudio))
            .Select(bible => new Response
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                Id = bible.Id,
                SerializedLicenseInfo = bible.LicenseInfo,
                LanguageId = bible.LanguageId,
                IsLanguageDefault = bible.LanguageDefault,
                HasAudio = bible.BibleBookContents.Any(bbc => bbc.AudioUrls != null),
            })
            .ToListAsync(ct);

        await SendOkAsync(bibles, ct);
    }
}