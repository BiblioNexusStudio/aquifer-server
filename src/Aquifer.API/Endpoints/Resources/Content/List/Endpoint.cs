using Aquifer.API.Common;
using Aquifer.Common.Extensions;
using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public class Endpoint(AquiferDbContext _dbContext, IResourceContentSearchService _resourceContentSearchService)
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

        var (total, resourceContentSummaries) = await _resourceContentSearchService.SearchAsync(
            new ResourceContentSearchFilter
            {
                ParentResourceId = req.ParentResourceId,
                ResourceEnglishLabelQuery = req.SearchQuery,
                LanguageId = req.LanguageId,
                ExcludeContentMediaTypes = [ResourceContentMediaType.Audio],
                ExcludeContentStatuses = [ResourceContentStatus.TranslationNotApplicable, ResourceContentStatus.CompleteNotApplicable],
                IsPublished = req.IsPublished,
                StartVerseId = verseRange?.startVerseId,
                EndVerseId = verseRange?.endVerseId,
                HasAudio = req.HasAudio,
                HasUnresolvedCommentThreads = req.HasUnresolvedCommentThreads,
            },
            req.Offset,
            req.Limit,
            ct);

        var languageIds = resourceContentSummaries
            .Select(rcs => rcs.LanguageId)
            .ToHashSet();

        var languageEnglishDisplayById = languageIds.Count == 0
            ? []
            : await _dbContext.Languages
                .Where(l => languageIds.Contains(l.Id))
                .Select(l => new
                {
                    l.Id,
                    l.EnglishDisplay,
                })
                .ToDictionaryAsync(l => l.Id, l => l.EnglishDisplay, ct);

        var response = new Response
        {
            ResourceContents = resourceContentSummaries
                .Select(rcs => new ResourceContentResponse
                {
                    Id = rcs.Id,
                    EnglishLabel = rcs.ResourceEnglishLabel,
                    ParentResourceName = rcs.ParentResourceEnglishDisplayName,
                    LanguageEnglishDisplay = languageEnglishDisplayById[rcs.LanguageId],
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