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
    public override void Configure()
    {
        Get("/resources/content");
        Permissions(PermissionName.ReadResourceLists);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var verseRange = BibleUtilities.VerseRangeForBookAndChapters(req.BookCode, req.StartChapter, req.EndChapter);

        // The opposite of IsPublished == true is not IsDraft == true (because the most recent ResourceContentVersion for a ResourceContent
        // can be neither of those statuses) but that logic is used here because consumers expect it.
        var (total, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            new ResourceContentSearchFilter
            {
                ParentResourceId = req.ParentResourceId,
                ResourceEnglishLabelQuery = req.SearchQuery,
                LanguageId = req.LanguageId,
                ExcludeContentMediaTypes = [ResourceContentMediaType.Audio],
                ExcludeContentStatuses = [ResourceContentStatus.TranslationNotApplicable, ResourceContentStatus.CompleteNotApplicable],
                IsPublished = req.IsPublished.HasValue && req.IsPublished.Value ? true : null,
                IsDraft = req.IsPublished.HasValue && !req.IsPublished.Value ? true : null,
                StartVerseId = verseRange?.startVerseId,
                EndVerseId = verseRange?.endVerseId,
                HasAudio = req.HasAudio,
                HasUnresolvedCommentThreads = req.HasUnresolvedCommentThreads,
            },
            req.Offset,
            req.Limit,
            shouldSortByName: true,
            ct);

        var languageEntityByIdMap = await _cachingLanguageService.GetLanguageEntityByIdMapAsync(ct);

        var response = new Response
        {
            ResourceContents = resourceContentSummaries
                .Select(rcs => new ResourceContentResponse
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEntityByIdMap[rcs.LanguageId].EnglishDisplay,
                    Status = rcs.Status.GetDisplayName(),
                    IsPublished = rcs.IsPublished,
                    HasAudio = rcs.HasAudio,
                    HasUnresolvedCommentThreads = rcs.HasUnresolvedCommentThreads,
                })
                .ToList(),
            Total = total,
        };

        await SendOkAsync(response, ct);
    }
}