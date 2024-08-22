using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Alignments.Greek.Book.Chapter;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, IEnumerable<Response>>
{
    private const string BaseQuery = """
                                     SELECT bvw.Id, bvw.WordIdentifier, bvw.[Text] AS EnglishWord, bvwgw.BibleVersionWordGroupId
                                     FROM BibleVersionWords bvw
                                     LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
                                     WHERE BibleId = @BibleId AND bvw.WordIdentifier BETWEEN @LowerBounds AND @UpperBounds
                                     ORDER BY bvw.WordIdentifier
                                     """;

    private const string GreekWordsQuery = """
                                           SELECT bvw.Id AS EnglishWordId, gntw.Id AS GreekNewTestamentWordId, gw.[Text] AS GreekWord
                                           FROM BibleVersionWords bvw
                                           LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
                                           LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = bvwgw.BibleVersionWordGroupId
                                           LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = nta.GreekNewTestamentWordGroupId
                                           LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
                                           LEFT JOIN GreekWords gw ON gntw.GreekWordId = gw.Id
                                           WHERE BibleId = @BibleId AND bvw.WordIdentifier BETWEEN @LowerBounds AND @UpperBounds AND gntw.Id IS NOT NULL
                                           """;

    private const string GreekSensesQuery = """
                                            WITH CTE AS (
                                                SELECT gntw.Id AS GreekNewTestamentWordId,
                                                       gs.DefinitionShort AS Definition,
                                                       gsg.[Text] AS Gloss,
                                                       ROW_NUMBER() OVER (PARTITION BY gntw.Id, gs.DefinitionShort, gsg.[Text] ORDER BY gntw.Id) AS RowNum
                                                FROM BibleVersionWords bvw
                                                LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
                                                LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = bvwgw.BibleVersionWordGroupId
                                                LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = nta.GreekNewTestamentWordGroupId
                                                LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
                                                LEFT JOIN GreekNewTestamentWordSenses gntws ON gntws.GreekNewTestamentWordId = gntw.Id
                                                LEFT JOIN GreekSenses gs ON gs.Id = gntws.GreekSenseId
                                                LEFT JOIN GreekSenseGlosses gsg ON gsg.GreekSenseId = gs.Id
                                                WHERE BibleId = @BibleId AND bvw.WordIdentifier BETWEEN @LowerBounds AND @UpperBounds AND gntw.Id IS NOT NULL AND gs.DefinitionShort IS NOT NULL
                                            )
                                            SELECT GreekNewTestamentWordId, Definition, STRING_AGG(Gloss, '||') AS Glosses
                                            FROM CTE
                                            WHERE RowNum = 1
                                            GROUP BY GreekNewTestamentWordId, Definition
                                            """;

    public override void Configure()
    {
        Get("/bibles/{BibleId}/alignments/greek/book/{BookCode}/chapter/{Chapter}");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bookCodeEnum = BibleBookCodeUtilities.IdFromCode(request.BookCode);
        var lowerBounds = AlignmentUtilities.LowerBoundOfChapter(bookCodeEnum, request.Chapter);
        var upperBounds = AlignmentUtilities.UpperBoundOfChapter(bookCodeEnum, request.Chapter);

        var baseQueryResults = await dbContext.Database
            .SqlQueryRaw<BaseQueryResult>(BaseQuery, new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds), new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        var greekWords = await dbContext.Database
            .SqlQueryRaw<GreekWords>(GreekWordsQuery, new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds), new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        var greekSenses = await dbContext.Database
            .SqlQueryRaw<GreekSenses>(GreekSensesQuery, new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds), new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        Response = baseQueryResults
            .GroupBy(r => (int)(r.WordIdentifier / 10000 % 1000))
            .Select(v => new Response
            {
                Verse = v.Key,
                Words = v.Select(w => new EnglishWordWithGreekAlignment
                {
                    Word = w.EnglishWord,
                    NextWordIsInGroup =
                        v.OrderBy(next => next.WordIdentifier).SkipWhile(next => next.Id != w.Id).Skip(1).FirstOrDefault()
                            ?.BibleVersionWordGroupId == w.BibleVersionWordGroupId && w.BibleVersionWordGroupId != null,
                    GreekWords = w.BibleVersionWordGroupId == null ||
                                 v.OrderBy(next => next.WordIdentifier)
                                     .First(next => next.BibleVersionWordGroupId == w.BibleVersionWordGroupId).Id == w.Id
                        ? greekWords
                            .Where(gw => gw.EnglishWordId == w.Id)
                            .Select(gw => new GreekWord
                            {
                                Word = gw.GreekWord,
                                Senses = greekSenses
                                    .Where(gs => gs.GreekNewTestamentWordId == gw.GreekNewTestamentWordId)
                                    .Select(gs => new GreekSense
                                    {
                                        Definition = gs.Definition,
                                        Glosses = gs.Glosses?.Split("||").Order().ToList() ?? []
                                    })
                            })
                        : []
                })
            });
    }

    public record BaseQueryResult
    {
        public int Id { get; set; }
        public long WordIdentifier { get; set; }
        public string EnglishWord { get; set; } = null!;
        public int? BibleVersionWordGroupId { get; set; }
    }

    public record GreekWords
    {
        public int EnglishWordId { get; set; }
        public int GreekNewTestamentWordId { get; set; }
        public string GreekWord { get; set; } = null!;
    }

    public record GreekSenses
    {
        public int GreekNewTestamentWordId { get; set; }
        public string Definition { get; set; } = null!;
        public string? Glosses { get; set; }
    }
}