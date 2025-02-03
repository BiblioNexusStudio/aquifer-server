using Aquifer.API.Common;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.ReviewPending.List;

public class Endpoint(IResourceContentSearchService _resourceContentSearchService, ICachingLanguageService _cachingLanguageService)
    : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/review-pending");
        Permissions(PermissionName.ReviewContent);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var (_, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            ResourceContentSearchIncludeFlags.Project |
            ResourceContentSearchIncludeFlags.HasAudioForLanguage |
            ResourceContentSearchIncludeFlags.HasUnresolvedCommentThreads,
            new ResourceContentSearchFilter
            {
                IsDraft = true,
                IncludeContentStatuses =
                [
                    ResourceContentStatus.AquiferizeReviewPending,
                    ResourceContentStatus.TranslationReviewPending,
                ],
                HasAudio = req.HasAudio,
                HasUnresolvedCommentThreads = req.HasUnresolvedCommentThreads,
            },
            ResourceContentSearchSortOrder.ResourceContentId,
            offset: 0,
            limit: null,
            ct);

        var languageByIdMap = await _cachingLanguageService.GetLanguageByIdMapAsync(ct);

        var response = resourceContentSummaries
            .Select(rcs =>
            {
                // Project may be null
                var project = rcs.Project;
                var resourceContentVersion = rcs.ResourceContentVersion!;

                return new Response
                {
                    Id = rcs.ResourceContent.Id,
                    EnglishLabel = rcs.Resource.EnglishLabel,
                    LanguageEnglishDisplay = languageByIdMap[rcs.ResourceContent.LanguageId].EnglishDisplay,
                    ParentResourceName = rcs.ParentResource.DisplayName,
                    LastStatusUpdate = rcs.ResourceContent.Updated,
                    ProjectName = project?.Name,
                    WordCount = resourceContentVersion.SourceWordCount,
                    SortOrder = rcs.Resource.SortOrder,
                    ContentUpdated = rcs.ResourceContent.ContentUpdated,
                    ReviewLevel = resourceContentVersion.ReviewLevel,
                    HasAudio = rcs.Resource.HasAudioForLanguage!.Value,
                    HasUnresolvedCommentThreads = resourceContentVersion.HasUnresolvedCommentThreads!.Value,
                };
            })
            .OrderByDescending(x => x.DaysSinceStatusChange)
            .ThenBy(x => x.EnglishLabel)
            .ToList();

        await SendOkAsync(response, ct);
    }
}