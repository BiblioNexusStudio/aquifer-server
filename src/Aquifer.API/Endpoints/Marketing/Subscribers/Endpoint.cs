using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Marketing.Subscribers;

public class Endpoint(AquiferDbContext dbContext, ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Put("/marketing/subscribers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var subscriber = await dbContext.ContentSubscribers.Where(x => x.Email == req.Email)
                .Include(x => x.ContentSubscriberLanguages)
                .Include(x => x.ContentSubscriberParentResources)
                .SingleOrDefaultAsync(ct);

            if (subscriber is not null)
            {
                await HandleExistingSubscriberAsync(req, subscriber, ct);
            }
            else
            {
                await HandleNewSubscriberAsync(req, ct);
            }

            await dbContext.SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to handle subscriber request: {requestContent}", JsonUtilities.DefaultSerialize(req));
        }
        finally
        {
            await SendNoContentAsync(ct);
        }
    }

    private async Task HandleExistingSubscriberAsync(Request req, ContentSubscriberEntity subscriber, CancellationToken ct)
    {
        var parentResourcesToAdd = req.SelectedParentResourceIds
            .Where(x => !subscriber.ContentSubscriberParentResources.Select(y => y.ParentResourceId).Contains(x))
            .Select(id => new ContentSubscriberParentResourceEntity
            {
                ContentSubscriberId = subscriber.Id,
                ParentResourceId = id
            });

        var parentResourcesToRemove =
            subscriber.ContentSubscriberParentResources.Where(x => !req.SelectedParentResourceIds.Contains(x.ParentResourceId));

        var languagesToAdd = req.SelectedLanguageIds
            .Where(x => !subscriber.ContentSubscriberLanguages.Select(y => y.LanguageId).Contains(x))
            .Select(id => new ContentSubscriberLanguageEntity
            {
                ContentSubscriberId = subscriber.Id,
                LanguageId = id
            });

        var languagesToRemove = subscriber.ContentSubscriberLanguages.Where(x => !req.SelectedLanguageIds.Contains(x.LanguageId));

        await dbContext.ContentSubscriberParentResources.AddRangeAsync(parentResourcesToAdd, ct);
        dbContext.ContentSubscriberParentResources.RemoveRange(parentResourcesToRemove);

        await dbContext.ContentSubscriberLanguages.AddRangeAsync(languagesToAdd, ct);
        dbContext.ContentSubscriberLanguages.RemoveRange(languagesToRemove);

        subscriber.GetNewsletter = req.GetNewsletter;
        subscriber.Name = req.Name;
        subscriber.Organization = req.Organization;
    }

    private async Task HandleNewSubscriberAsync(Request req, CancellationToken ct)
    {
        await dbContext.ContentSubscribers.AddAsync(new ContentSubscriberEntity
            {
                Name = req.Name,
                Email = req.Email,
                Organization = req.Organization,
                GetNewsletter = req.GetNewsletter,
                ContentSubscriberParentResources =
                    req.SelectedParentResourceIds.Select(id => new ContentSubscriberParentResourceEntity { ParentResourceId = id })
                        .ToList(),
                ContentSubscriberLanguages = req.SelectedLanguageIds.Select(id => new ContentSubscriberLanguageEntity { LanguageId = id })
                    .ToList()
            },
            ct);
    }
}