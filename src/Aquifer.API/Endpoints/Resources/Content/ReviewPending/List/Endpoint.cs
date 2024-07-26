using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.ReviewPending.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/review-pending");
        Permissions(PermissionName.ReviewContent);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var resourceContents = (await dbContext.ResourceContentVersions
                .Where(rcv => rcv.IsDraft &&
                              (rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeReviewPending ||
                               rcv.ResourceContent.Status == ResourceContentStatus.TranslationReviewPending))
                .Select(x => new Response
                {
                    Id = x.ResourceContentId,
                    EnglishLabel = x.ResourceContent.Resource.EnglishLabel,
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    LastStatusUpdate = x.ResourceContent.Updated,
                    ProjectName =
                        x.ResourceContent.Projects.FirstOrDefault() == null ? null : x.ResourceContent.Projects.FirstOrDefault()!.Name,
                    WordCount = x.SourceWordCount,
                    SortOrder = x.ResourceContent.Resource.SortOrder,
                    ContentUpdated = x.ResourceContent.ContentUpdated
                }).ToListAsync(ct))
            .OrderByDescending(x => x.DaysSinceStatusChange).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}