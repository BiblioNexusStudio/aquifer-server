﻿using Aquifer.API.Helpers;
using Aquifer.Common;
using Aquifer.Common.Clients;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Aquifer.API.Endpoints.Marketing.Subscribers.Options;

public class Endpoint(AquiferDbContext dbContext, ISendGridClient sendGridClient) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/marketing/subscribers/options");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        ResponseCache(EndpointHelpers.OneHourInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.ParentResourceOptions = await dbContext.ParentResources.Where(x => x.ForMarketing == true)
            .Select(x => new SubscriberOption { Id = x.Id, EnglishDisplayName = x.DisplayName })
            .ToListAsync(ct);

        Response.LanguageOptions = await dbContext.Languages.Select(x => new SubscriberOption
            {
                Id = x.Id, EnglishDisplayName = x.EnglishDisplay
            })
            .ToListAsync(ct);
    }
}