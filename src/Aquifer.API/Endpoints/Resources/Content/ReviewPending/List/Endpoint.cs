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
        var resourceContents =
            (await dbContext.ResourceContentVersions
                .Where(rcv =>
                    rcv.IsDraft &&
                    (rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeReviewPending ||
                    rcv.ResourceContent.Status == ResourceContentStatus.TranslationReviewPending))
                .Select(rcv => new Response
                {
                    Id = rcv.ResourceContentId,
                    EnglishLabel = rcv.ResourceContent.Resource.EnglishLabel,
                    LanguageEnglishDisplay = rcv.ResourceContent.Language.EnglishDisplay,
                    ParentResourceName = rcv.ResourceContent.Resource.ParentResource.DisplayName,
                    LastStatusUpdate = rcv.ResourceContent.Updated,
                    ProjectName = rcv.ResourceContent.ProjectResourceContents.FirstOrDefault() == null
                        ? null
                        : rcv.ResourceContent.ProjectResourceContents.First().Project.Name,
                    WordCount = rcv.SourceWordCount,
                    SortOrder = rcv.ResourceContent.Resource.SortOrder,
                    ContentUpdated = rcv.ResourceContent.ContentUpdated,
                    ReviewLevel = rcv.ReviewLevel,
                })
                .ToListAsync(ct))
            .OrderByDescending(x => x.DaysSinceStatusChange)
            .ThenBy(x => x.EnglishLabel)
            .ToList();

        await SendOkAsync(resourceContents, ct);
    }
}