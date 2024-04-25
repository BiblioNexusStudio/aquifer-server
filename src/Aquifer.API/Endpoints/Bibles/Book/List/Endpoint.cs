﻿using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Book.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.BibleBooks.Where(x => x.BibleId == req.BibleId).Select(x => new Response
        {
            Id = x.Id,
            Code = x.Code,
            Number = (int)x.Number,
            LocalizedName = x.LocalizedName,
            TotalChapters = x.Chapters.Count,
            Chapters = x.Chapters.OrderBy(c => c.Number).Select(c => new ResponseChapter
            {
                Number = c.Number,
                TotalVerses = c.Verses.Count
            })
        }).ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}