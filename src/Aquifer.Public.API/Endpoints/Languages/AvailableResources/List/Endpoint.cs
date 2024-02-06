using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/languages/available-resources");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var (startVerseId, endVerseId) = req.BookId.HasValue
            ? BibleUtilities.GetBookVerseRange(req.BookId.Value)
            : (req.StartVerseId!.Value, req.EndVerseId ?? req.StartVerseId.Value);

        var response = await dbContext.ResourceContents.Where(x =>
                x.Versions.Any(v => v.IsPublished) &&
                (x.Resource.VerseResources.Any(vr =>
                        vr.VerseId >= startVerseId && vr.VerseId <= endVerseId) ||
                    x.Resource.PassageResources.Any(pr =>
                        (pr.Passage.StartVerseId >= startVerseId && pr.Passage.StartVerseId <= endVerseId) ||
                        (pr.Passage.EndVerseId >= startVerseId && pr.Passage.EndVerseId <= endVerseId) ||
                        (pr.Passage.StartVerseId <= startVerseId && pr.Passage.EndVerseId >= endVerseId))))
            .GroupBy(x => x.Language)
            .Select(x => new Response
            {
                LanguageCode = x.Key.ISO6393Code,
                LanguageId = x.Key.Id,
                ResourceCounts = x.GroupBy(rc => rc.Resource.ParentResource.ResourceType)
                    .Select(rc => new ResourceCountByType
                    {
                        Type = rc.Key.ToString(),
                        Count = rc.Count()
                    }).ToList()
            }).ToListAsync(ct);

        await SendAsync(response, 200, ct);
    }
}