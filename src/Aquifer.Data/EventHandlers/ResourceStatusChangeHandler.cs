using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class ResourceStatusChangeHandler
{
    private static readonly List<ResourceContentStatus> InReviewOrGreaterStatuses =
    [
        ResourceContentStatus.Complete, ResourceContentStatus.AquiferizePublisherReview, ResourceContentStatus.TranslationPublisherReview
    ];

    public static async Task HandleAsync(DbContextOptions<AquiferDbContext> dbContextOptions, IEnumerable<EntityEntry> entityEntries)
    {
        List<int> completedContentIds = [];
        List<int> inReviewContentIds = [];
        List<int> inProgressIds = [];

        entityEntries.Where(entry => entry is { State: EntityState.Unchanged, Entity: ResourceContentEntity })
            .Select(x => x.Entity as ResourceContentEntity)
            .ToList()
            .ForEach(x =>
            {
                switch (x?.Status)
                {
                    case ResourceContentStatus.Complete:
                        completedContentIds.Add(x.Id);
                        break;
                    case ResourceContentStatus.AquiferizePublisherReview or ResourceContentStatus.TranslationPublisherReview:
                        inReviewContentIds.Add(x.Id);
                        break;
                    case ResourceContentStatus.TranslationInProgress:
                    case ResourceContentStatus.AquiferizeInProgress:
                        inProgressIds.Add(x.Id);
                        break;
                }
            });

        if (completedContentIds.Count + inReviewContentIds.Count + inProgressIds.Count == 0)
        {
            return;
        }

        await using var dbContext = new AquiferDbContext(dbContextOptions);

        if (completedContentIds.Count > 0)
        {
            await dbContext.Projects.Where(x =>
                    x.ProjectResourceContents.Any(prc => completedContentIds.Contains(prc.ResourceContent.Id)) &&
                    x.ProjectResourceContents.Where(prc => !completedContentIds.Contains(prc.ResourceContent.Id))
                        .All(prc => prc.ResourceContent.Status == ResourceContentStatus.Complete))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ActualPublishDate, DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(p => p.Updated, DateTime.UtcNow));
        }

        if (inReviewContentIds.Count > 0)
        {
            await dbContext.Projects.Where(x =>
                    x.ProjectResourceContents.Any(prc => inReviewContentIds.Contains(prc.ResourceContent.Id)) &&
                    x.ProjectResourceContents.Where(prc => !inReviewContentIds.Contains(prc.ResourceContent.Id))
                        .All(prc => InReviewOrGreaterStatuses.Contains(prc.ResourceContent.Status)))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ActualDeliveryDate, DateOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(p => p.Updated, DateTime.UtcNow));
        }

        if (inProgressIds.Count > 0)
        {
            await dbContext.Projects.Where(x =>
                    x.ActualDeliveryDate != null &&
                    x.ActualPublishDate == null &&
                    x.ProjectResourceContents.Any(prc => inProgressIds.Contains(prc.ResourceContent.Id)) &&
                    x.ProjectResourceContents.Where(prc => !inProgressIds.Contains(prc.ResourceContent.Id)).All(prc => InReviewOrGreaterStatuses.Contains(prc.ResourceContent.Status)))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ActualDeliveryDate, null as DateOnly?)
                    .SetProperty(p => p.Updated, DateTime.UtcNow));
        }
    }
}