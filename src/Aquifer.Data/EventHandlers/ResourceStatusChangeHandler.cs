using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class ResourceStatusChangeHandler
{
    private static readonly List<ResourceContentStatus> s_completedStatuses =
    [
        ResourceContentStatus.Complete, ResourceContentStatus.CompleteNotApplicable
    ];

    private static readonly List<ResourceContentStatus> s_inReviewOrGreaterStatuses =
    [
        ResourceContentStatus.Complete, ResourceContentStatus.AquiferizePublisherReview, ResourceContentStatus.TranslationPublisherReview,
        ResourceContentStatus.CompleteNotApplicable, ResourceContentStatus.TranslationNotApplicable
    ];

    public static async Task HandleAsync(DbContextOptions dbContextOptions, IEnumerable<EntityEntry> entityEntries)
    {
        List<int> completedContentIds = [];
        List<int> inReviewContentIds = [];
        List<int> editorReviewIds = [];

        entityEntries.Where(entry => entry is { State: EntityState.Unchanged, Entity: ResourceContentEntity })
            .Select(x => x.Entity as ResourceContentEntity)
            .ToList()
            .ForEach(
                x =>
                {
#pragma warning disable IDE0010 // Add missing cases to switch statement
                    switch (x?.Status)
                    {
                        case ResourceContentStatus.Complete or ResourceContentStatus.CompleteNotApplicable:
                            completedContentIds.Add(x.Id);
                            break;
                        case ResourceContentStatus.AquiferizePublisherReview or ResourceContentStatus.TranslationPublisherReview
                            or ResourceContentStatus.TranslationNotApplicable:
                            inReviewContentIds.Add(x.Id);
                            break;
                        case ResourceContentStatus.TranslationEditorReview:
                        case ResourceContentStatus.AquiferizeEditorReview:
                            editorReviewIds.Add(x.Id);
                            break;
                    }
#pragma warning restore IDE0010
                });

        if (completedContentIds.Count + inReviewContentIds.Count + editorReviewIds.Count == 0)
        {
            return;
        }

        await using var dbContext = new AquiferDbContext(dbContextOptions, false);

        if (completedContentIds.Count > 0)
        {
            await dbContext.Projects
                .Where(
                    x => x.ProjectResourceContents.Any(prc => completedContentIds.Contains(prc.ResourceContent.Id)) &&
                        x.ProjectResourceContents
                            .Where(prc => !completedContentIds.Contains(prc.ResourceContent.Id))
                            .All(prc => s_completedStatuses.Contains(prc.ResourceContent.Status)))
                .ExecuteUpdateAsync(
                    x => x.SetProperty(p => p.ActualPublishDate, DateOnly.FromDateTime(DateTime.UtcNow))
                        .SetProperty(p => p.Updated, DateTime.UtcNow));
        }

        if (inReviewContentIds.Count > 0)
        {
            await dbContext.Projects
                .Where(
                    x => x.ProjectResourceContents.Any(prc => inReviewContentIds.Contains(prc.ResourceContent.Id)) &&
                        x.ProjectResourceContents
                            .Where(prc => !inReviewContentIds.Contains(prc.ResourceContent.Id))
                            .All(prc => s_inReviewOrGreaterStatuses.Contains(prc.ResourceContent.Status)))
                .ExecuteUpdateAsync(
                    x => x.SetProperty(p => p.ActualDeliveryDate, DateOnly.FromDateTime(DateTime.UtcNow))
                        .SetProperty(p => p.Updated, DateTime.UtcNow));
        }

        if (editorReviewIds.Count > 0)
        {
            await dbContext.Projects
                .Where(
                    x => x.ActualDeliveryDate != null &&
                        x.ActualPublishDate == null &&
                        x.ProjectResourceContents.Any(prc => editorReviewIds.Contains(prc.ResourceContent.Id)) &&
                        x.ProjectResourceContents
                            .Where(prc => !editorReviewIds.Contains(prc.ResourceContent.Id))
                            .All(prc => s_inReviewOrGreaterStatuses.Contains(prc.ResourceContent.Status)))
                .ExecuteUpdateAsync(
                    x => x.SetProperty(p => p.ActualDeliveryDate, null as DateOnly?).SetProperty(p => p.Updated, DateTime.UtcNow));
        }
    }
}