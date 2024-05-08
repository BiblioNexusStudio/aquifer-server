using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.GroupedByVerse.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    private const string Query = """
        SELECT
            rc.Id,
            vr.VerseId,
            rc.MediaType,
            pr.ResourceType,
            pr.ShortName
        FROM
            ResourceContents rc
            INNER JOIN Resources r ON r.id = rc.ResourceId
            INNER JOIN ParentResources pr ON pr.Id = r.ParentResourceId
            INNER JOIN VerseResources vr ON vr.ResourceId = r.Id
        WHERE
            rc.LanguageId = @LanguageId
            AND(vr.VerseId BETWEEN @StartVerseId AND @EndVerseId)

        UNION

        SELECT
            rc.Id,
            v.Id AS VerseId,
            rc.MediaType,
            parr.ResourceType,
            parr.ShortName
        FROM
            ResourceContents rc
            INNER JOIN Resources r ON r.Id = rc.ResourceId
            INNER JOIN ParentResources parr ON parr.Id = r.ParentResourceId
            INNER JOIN PassageResources pasr ON pasr.ResourceId = r.id
            INNER JOIN Passages p ON p.Id = pasr.PassageId
            INNER JOIN Verses v ON v.Id BETWEEN p.StartVerseId AND p.EndVerseId
                AND v.Id BETWEEN @StartVerseId AND @EndVerseId
        WHERE
            rc.LanguageId = @LanguageId
            AND((p.StartVerseId BETWEEN @StartVerseId AND @EndVerseId)
            OR(p.EndVerseId BETWEEN @StartVerseId AND @EndVerseId)
            OR(p.StartVerseId < @StartVerseId AND p.EndVerseId > @EndVerseId))

        ORDER BY
            VerseId
    """;

    public override void Configure()
    {
        Get("/resources/content/grouped-by-verse");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var (startVerseId, endVerseId) =
                    BibleUtilities.VerseRangeForBookAndChapters(request.BookCode!, request.Chapter, request.Chapter)!.Value;

        var rows = await dbContext.Database
            .SqlQueryRaw<ResourceContentRow>(Query,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("StartVerseId", startVerseId),
                new SqlParameter("EndVerseId", endVerseId))
            .ToListAsync(ct);

        var groupedRows = rows.GroupBy(row => row.VerseId);

        var response = new Response
        {
            Verses = groupedRows.Select(group =>
                new ResourcesForVerseResponse
                {
                    Number = BibleUtilities.TranslateVerseId(group.Key).verse,
                    ResourceContents = group.Select(row => new ResourceContentResponse
                    {
                        Id = row.Id,
                        MediaType = row.MediaType.ToString(),
                        ParentResource = row.ShortName,
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
    public required ResourceContentMediaType MediaType { get; set; }
    public required ResourceType ResourceType { get; set; }
    public required string ShortName { get; set; }
}