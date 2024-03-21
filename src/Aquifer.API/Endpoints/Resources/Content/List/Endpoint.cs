using Aquifer.Common.Extensions;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var query = dbContext.ResourceContents.Where(rc => rc.MediaType != ResourceContentMediaType.Audio);

        query = ApplyLanguageIdFilter(query, request.LanguageId);
        query = ApplyParentResourceIdFilter(query, request.ParentResourceId);
        query = ApplyBookAndChapterFilter(query, request.BookCode, request.StartChapter, request.EndChapter);
        query = ApplyIsPublishedFilter(query, request.IsPublished);
        query = ApplySearchQueryFilter(query, request.SearchQuery);

        var resourceContent = await query
            .OrderBy(rc => rc.Resource.EnglishLabel)
            .Skip(request.Offset).Take(request.Limit)
            .Select(rc => new ResourceContentResponse
            {
                Id = rc.Id,
                EnglishLabel = rc.Resource.EnglishLabel,
                ParentResourceName = rc.Resource.ParentResource.DisplayName,
                LanguageEnglishDisplay = rc.Language.EnglishDisplay,
                Status = rc.Status.GetDisplayName(),
                IsPublished = rc.Versions.Any(v => v.IsPublished)
            }).ToListAsync(ct);

        var total = await query.CountAsync(ct);

        await SendOkAsync(new Response { ResourceContents = resourceContent, Total = total }, ct);
    }

    private IQueryable<ResourceContentEntity> ApplyLanguageIdFilter(IQueryable<ResourceContentEntity> query, int? languageId)
    {
        if (languageId.HasValue)
        {
            query = query.Where(rc => rc.LanguageId == languageId);
        }

        return query;
    }

    private IQueryable<ResourceContentEntity> ApplyParentResourceIdFilter(IQueryable<ResourceContentEntity> query, int? parentResourceId)
    {
        if (parentResourceId.HasValue)
        {
            query = query.Where(rc => rc.Resource.ParentResourceId == parentResourceId);
        }

        return query;
    }

    private IQueryable<ResourceContentEntity> ApplyBookAndChapterFilter(IQueryable<ResourceContentEntity> query, string? bookCode,
        int? startChapter, int? endChapter)
    {
        var verseRange = BibleUtilities.VerseRangeForBookAndChapters(bookCode, startChapter, endChapter);

        if (verseRange is not null)
        {
            var startVerseId = verseRange.Value.Item1;
            var endVerseId = verseRange.Value.Item2;

            query = query.Where(rc =>
                rc.Resource.VerseResources.Any(vr =>
                    vr.VerseId >= startVerseId && vr.VerseId <= endVerseId) ||
                rc.Resource.PassageResources.Any(pr =>
                    (pr.Passage.StartVerseId >= startVerseId && pr.Passage.StartVerseId <= endVerseId) ||
                    (pr.Passage.EndVerseId >= startVerseId && pr.Passage.EndVerseId <= endVerseId) ||
                    (pr.Passage.StartVerseId <= startVerseId && pr.Passage.EndVerseId >= endVerseId)));
        }

        return query;
    }

    private IQueryable<ResourceContentEntity> ApplyIsPublishedFilter(IQueryable<ResourceContentEntity> query, bool? isPublished)
    {
        if (isPublished.HasValue)
        {
            query = isPublished.Value
                ? query.Where(rc => rc.Versions.Any(v => v.IsPublished))
                : query.Where(rc => rc.Versions.All(v => !v.IsPublished));
        }

        return query;
    }

    private IQueryable<ResourceContentEntity> ApplySearchQueryFilter(IQueryable<ResourceContentEntity> query, string? searchQuery)
    {
        if (searchQuery is not null)
        {
            query = searchQuery[0].Equals('"') && searchQuery[^1].Equals('"')
                ? query.Where(rc => rc.Resource.EnglishLabel.Equals(searchQuery.Replace("\"", "")))
                : query.Where(rc => rc.Resource.EnglishLabel.Contains(searchQuery));
        }

        return query;
    }
}