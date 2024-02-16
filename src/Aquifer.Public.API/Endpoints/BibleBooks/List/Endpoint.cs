﻿using Aquifer.Common.Utilities;
using Aquifer.Public.API.Helpers;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.BibleBooks.List;

public class Endpoint : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/bible-books");
        Options(EndpointHelpers.SetCacheOption(60));

        Summary(s =>
        {
            s.Summary = "Get a list of Bible books.";
            s.Description =
                "Returns the list of Bible books in the system and their corresponding book number to be used in other calls.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = BibleBookCodeUtilities.GetAll().Select(x => new Response
        {
            Name = x.BookFullName,
            Code = x.BookCode
        });

        await SendAsync(response, 200, ct);
    }
}