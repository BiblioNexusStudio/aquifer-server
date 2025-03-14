using Aquifer.API.Common;
using Aquifer.Common.Extensions;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public class Endpoint(IResourceContentSearchService _resourceContentSearchService, ICachingLanguageService _cachingLanguageService)
    : Endpoint<Request, Response>
{
    private static readonly IReadOnlyList<ResourceContentStatus> s_notApplicableResourceContentStatuses =
    [
        ResourceContentStatus.TranslationNotApplicable,
        ResourceContentStatus.CompleteNotApplicable,
    ];

    public override void Configure()
    {
        Get("/resources/content");
        Permissions(PermissionName.ReadResourceLists);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var verseRange = BibleUtilities.VerseRangeForBookAndChapters(req.BookCode, req.StartChapter, req.EndChapter);

        var (total, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            ResourceContentSearchIncludeFlags.ResourceContentVersions |
                ResourceContentSearchIncludeFlags.HasAudioForLanguage |
                ResourceContentSearchIncludeFlags.HasUnresolvedCommentThreads,
            new ResourceContentSearchFilter
            {
                ParentResourceId = req.ParentResourceId,
                ResourceEnglishLabelQuery = req.SearchQuery,
                LanguageId = req.LanguageId,
                ExcludeContentMediaTypes = [ResourceContentMediaType.Audio],
                ExcludeContentStatuses = req.IsNotApplicable.HasValue && req.IsNotApplicable.Value
                    ? null
                    : s_notApplicableResourceContentStatuses,
                IncludeContentStatuses = req.IsNotApplicable.HasValue && req.IsNotApplicable.Value
                    ? s_notApplicableResourceContentStatuses
                    : null,
                IsPublished = req.IsPublished,
                VerseIdRanges = verseRange.HasValue ? [new VerseIdRange(verseRange.Value)] : null,
                HasAudio = req.HasAudio,
                HasUnresolvedCommentThreads = req.HasUnresolvedCommentThreads,
            },
            ResourceContentSearchSortOrder.ParentResourceAndResourceName,
            req.Offset,
            req.Limit,
            ct);

        var languageByIdMap = await _cachingLanguageService.GetLanguageByIdMapAsync(ct);

        var response = new Response
        {
            ResourceContents = resourceContentSummaries
                .Select(rcs => new ResourceContentResponse
                {
                    Id = rcs.ResourceContent.Id,
                    EnglishLabel = rcs.Resource.EnglishLabel,
                    ParentResourceName = rcs.ParentResource.DisplayName,
                    LanguageEnglishDisplay = languageByIdMap[rcs.ResourceContent.LanguageId].EnglishDisplay,
                    Status = rcs.ResourceContent.Status.GetDisplayName(),
                    IsPublished = rcs.ResourceContentVersions!.AnyIsPublished,
                    HasAudio = rcs.Resource.HasAudioForLanguage!.Value,
                    HasUnresolvedCommentThreads = rcs.ResourceContentVersions!.HasUnresolvedCommentThreads!.Value,
                })
                .ToList(),
            Total = total,
        };

        await SendOkAsync(response, ct);
    }
}