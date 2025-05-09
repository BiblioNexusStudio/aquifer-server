using Aquifer.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Search;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/search");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resources = await GetResourcesAsync(request, ct);

        await SendOkAsync(resources, ct);
    }

    private async Task<List<Response>> GetResourcesAsync(Request req, CancellationToken ct)
    {
        var (startVerseId, endVerseId) = req.BookCode is null
            ? ((int?)null, (int?)null)
            : BibleUtilities.GetVerseIds(req.BookCode, req.StartChapter, req.EndChapter, req.StartVerse, req.EndVerse);

        var query = dbContext.ResourceContentVersions
            .Where(x =>
                x.IsPublished &&
                x.ResourceContent.Resource.ParentResource.Enabled &&
                (req.Query == null || x.DisplayName.Contains(req.Query) || x.ResourceContent.Resource.EnglishLabel.Contains(req.Query)) &&
                (startVerseId == null ||
                    x.ResourceContent.Resource.VerseResources.Any(vr =>
                        vr.VerseId >= startVerseId && vr.VerseId <= endVerseId) ||
                    x.ResourceContent.Resource.PassageResources.Any(pr =>
                        (pr.Passage.StartVerseId >= startVerseId && pr.Passage.StartVerseId <= endVerseId) ||
                        (pr.Passage.EndVerseId >= startVerseId && pr.Passage.EndVerseId <= endVerseId) ||
                        (pr.Passage.StartVerseId <= startVerseId && pr.Passage.EndVerseId >= endVerseId))) &&
                (req.ParentResourceId == x.ResourceContent.Resource.ParentResourceId ||
                    req.ResourceTypes.Contains(x.ResourceContent.Resource.ParentResource.ResourceType)) &&
                // Either it's in the requested language or it's allowed to fallback to English and has no resource in requested language
                (x.ResourceContent.LanguageId == req.LanguageId ||
                    (Constants.FallbackToEnglishForMediaTypes.Contains(x.ResourceContent.MediaType) &&
                        x.ResourceContent.LanguageId == 1 &&
                        !x.ResourceContent.Resource.ResourceContents.Any(rc => rc.LanguageId == req.LanguageId))))
            .OrderBy(r => r.ResourceContent.Resource.SortOrder)
            .ThenBy(r => r.DisplayName)
            .Select(rcv => new Response
            {
                Id = rcv.ResourceContentId,
                DependentOnId =
                    rcv.ResourceContent.MediaType != ResourceContentMediaType.Text
                        ? rcv.ResourceContent
                            .Resource
                            .ResourceContents
                            .Where(rc => rc.LanguageId == req.LanguageId && rc.MediaType == ResourceContentMediaType.Text)
                            .Select(rc => rc.Id)
                            .FirstOrDefault()
                        : null,
                DisplayName = rcv.DisplayName,
                MediaType = rcv.ResourceContent.MediaType.ToString(),
                ParentResourceId = rcv.ResourceContent.Resource.ParentResourceId,
                SortOrder = rcv.ResourceContent.Resource.SortOrder,
                Version = rcv.Version,
                ResourceType = rcv.ResourceContent.Resource.ParentResource.ResourceType.ToString(),
            });

        if (req.ParentResourceId is not null)
        {
            query = query.Skip(req.Offset).Take(req.Limit);
        }

        return await query.ToListAsync(ct);
    }
}