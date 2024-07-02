using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AvailableChapters.List;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    private const string Query = """
                                     SELECT
                                         FLOOR(vr.VerseId / 1000000) % 1000 AS Book,
                                         FLOOR((vr.VerseId % 1000000) / 1000) AS Chapter
                                     FROM
                                         ResourceContents rc
                                         INNER JOIN Resources r ON r.id = rc.ResourceId
                                         INNER JOIN ParentResources pr ON pr.Id = r.ParentResourceId
                                         INNER JOIN VerseResources vr ON vr.ResourceId = r.Id
                                     WHERE
                                         rc.LanguageId = @LanguageId AND pr.Id = @ParentResourceId AND pr.Enabled = 1
                                 
                                     UNION
                                 
                                     SELECT
                                         FLOOR(v.Id / 1000000) % 1000 AS Book,
                                         FLOOR((v.Id % 1000000) / 1000) AS Chapter
                                     FROM
                                         ResourceContents rc
                                         INNER JOIN Resources r ON r.Id = rc.ResourceId
                                         INNER JOIN ParentResources parr ON parr.Id = r.ParentResourceId
                                         INNER JOIN PassageResources pasr ON pasr.ResourceId = r.id
                                         INNER JOIN Passages p ON p.Id = pasr.PassageId
                                         INNER JOIN Verses v ON v.Id BETWEEN p.StartVerseId AND p.EndVerseId
                                     WHERE
                                         rc.LanguageId = @LanguageId AND parr.Id = @ParentResourceId AND parr.Enabled = 1
                                 
                                     ORDER BY
                                         Book, Chapter
                                 """;

    public override void Configure()
    {
        Get("/resources/content/available-chapters");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var rows = await dbContext.Database
            .SqlQueryRaw<BookAndChapterRow>(Query,
                new SqlParameter("LanguageId", request.LanguageId),
                new SqlParameter("ParentResourceId", request.ParentResourceId))
            .ToListAsync(ct);

        var response = rows.GroupBy(row => row.Book).Select(row => new Response
        {
            BookCode = BibleBookCodeUtilities.CodeFromId((BookId)row.Key),
            Chapters = row.Select(r => r.Chapter).ToList()
        });

        await SendOkAsync(response, ct);
    }
}

public record BookAndChapterRow
{
    public required int Book { get; set; }
    public required int Chapter { get; set; }
}