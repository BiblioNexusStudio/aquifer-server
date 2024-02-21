using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.List.ReviewPending;

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
                    ContentId = x.ResourceContentId,
                    DisplayName = x.DisplayName,
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    HistoryCreated = x.ResourceContentVersionStatusHistories.Max(auh => auh.Created),
                    WordCount = x.WordCount
                }).ToListAsync(ct))
            .OrderByDescending(x => x.DaysSinceStatusChange).ThenBy(x => x.DisplayName).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}