using Aquifer.API.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/grouped-by-verse");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var (startVerseId, endVerseId) =
            BibleUtilities.VerseRangeForBookAndChapters(request.BookCode, request.Chapter, request.Chapter)!.Value;

        var fallbackMediaTypesSqlArray = string.Join(',', Constants.FallbackToEnglishForMediaTypes.Select(t => (int)t));

        var query = $"""
                         SELECT
                             COALESCE(rc.Id, rce.Id) AS Id,
                             vr.VerseId,
                             COALESCE(rc.MediaType, rce.MediaType) AS MediaType,
                             pr.ResourceType,
                             rcv.Version,
                             pr.Id AS ParentResourceId
                         FROM
                             VerseResources vr
                             INNER JOIN Resources r ON vr.ResourceId = r.Id
                             INNER JOIN ParentResources pr ON r.ParentResourceId = pr.Id
                             LEFT JOIN ResourceContents rc ON rc.ResourceId = r.Id AND rc.LanguageId = {request.LanguageId}
                             LEFT JOIN ResourceContents rce ON rce.ResourceId = r.Id AND rce.LanguageId = 1
                                                               AND rce.MediaType IN ({fallbackMediaTypesSqlArray})
                             INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = COALESCE(rc.Id, rce.Id) AND rcv.IsPublished = 1
                        WHERE
                             vr.VerseId BETWEEN {startVerseId} AND {endVerseId}
                     
                         UNION
                     
                         SELECT
                             COALESCE(rc.Id, rce.Id) AS Id,
                             v.Id AS VerseId,
                             COALESCE(rc.MediaType, rce.MediaType) AS MediaType,
                             parr.ResourceType,
                             rcv.Version,
                             parr.Id AS ParentResourceId
                         FROM
                             Resources r
                             INNER JOIN ParentResources parr ON parr.Id = r.ParentResourceId
                             INNER JOIN PassageResources pasr ON pasr.ResourceId = r.id
                             INNER JOIN Passages p ON p.Id = pasr.PassageId
                             INNER JOIN Verses v ON v.Id BETWEEN p.StartVerseId AND p.EndVerseId
                                 AND v.Id BETWEEN {startVerseId} AND {endVerseId}
                             LEFT JOIN ResourceContents rc ON rc.ResourceId = r.Id AND rc.LanguageId = {request.LanguageId}
                             LEFT JOIN ResourceContents rce ON rce.ResourceId = r.Id AND rce.LanguageId = 1
                                                               AND rce.MediaType IN ({fallbackMediaTypesSqlArray})
                             INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = COALESCE(rc.Id, rce.Id) AND rcv.IsPublished = 1
                         WHERE
                             (p.StartVerseId BETWEEN {startVerseId} AND {endVerseId})
                             OR (p.EndVerseId BETWEEN {startVerseId} AND {endVerseId})
                             OR (p.StartVerseId < {startVerseId} AND p.EndVerseId > {endVerseId})
                     
                         ORDER BY
                             VerseId
                     """;

        var rows = await dbContext.Database.SqlQueryRaw<ResourceContentRow>(query).ToListAsync(ct);

        var response = new Response
        {
            Verses = rows.GroupBy(row => row.VerseId).Select(group =>
                new ResourcesForVerseResponse
                {
                    Number = BibleUtilities.TranslateVerseId(group.Key).verse,
                    ResourceContents = group.Select(row => new ResourceContentResponse
                    {
                        Id = row.Id,
                        MediaType = row.MediaType.ToString(),
                        ParentResourceId = row.ParentResourceId,
                        Version = row.Version,
                        ResourceType = row.ResourceType.ToString()
                    })
                        .DistinctBy(row => row.Id)
                        .ToList()
                }).ToList()
        };

        await SendOkAsync(response, ct);
    }
}

public record ResourceContentRow
{
    public required int Id { get; set; }
    public required int VerseId { get; set; }
    public required int Version { get; set; }
    public required ResourceContentMediaType MediaType { get; set; }
    public required ResourceType ResourceType { get; set; }
    public required int ParentResourceId { get; set; }
}