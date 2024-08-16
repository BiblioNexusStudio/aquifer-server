using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Marketing.Subscribers.Choices;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/marketing/subscribers/choices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.ParentResourceChoices = await dbContext.ParentResources.Where(x => x.ForMarketing == true)
            .Select(x => new SubscriberChoice
            {
                Id = x.Id,
                EnglishDisplayName = x.DisplayName
            }).ToListAsync(ct);

        Response.LanguageChoices = await dbContext.Languages.Select(x => new SubscriberChoice
        {
            Id = x.Id,
            EnglishDisplayName = x.EnglishDisplay
        }).ToListAsync(ct);
    }
}