using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Public.API.Helpers;
using FastEndpoints;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    private const string GreekAlignmentSourceQuery = """
        SELECT TOP 1 gnt.[Name]
        FROM BibleVersionWords bvw
        LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
        LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = bvwgw.BibleVersionWordGroupId
        LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = nta.GreekNewTestamentWordGroupId
        LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
        LEFT JOIN GreekNewTestaments gnt ON gntw.GreekNewTestamentId = gnt.Id
        WHERE BibleId = @BibleId
        """;

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
        Get("/bibles/{BibleId}/alignments/greek");
        Options(EndpointHelpers.UnauthenticatedServerCacheInSeconds(EndpointHelpers.OneHourInSeconds));
        Description(d => d.ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Gets a Bible's Greek alignment information.";
            s.Description =
                "For a given Bible and book of the Bible, returns the Bible's text along with associated Greek alignment information and Greek sense data for each verse, similar to a reverse interlinear Bible. Data is returned for all verses within the chapter and verse parameters.";
        });
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bookCodeEnum = BibleBookCodeUtilities.IdFromCode(request.BookCode);
        var lowerBounds = new BibleWordIdentifier(bookCodeEnum, request.StartChapter, request.StartVerse, request.StartWord).WordIdentifier;
        var upperBounds = BibleWordIdentifier.GetUpperBoundOfWord(bookCodeEnum, request.EndChapter, request.EndVerse, request.EndWord).WordIdentifier;

        var bookData = await dbContext.BibleBooks
            .Where(bb =>
                bb.Bible.Enabled &&
                !bb.Bible.RestrictedLicense &&
                bb.Bible.GreekAlignment &&
                bb.Bible.Id == request.BibleId &&
                bb.Code == request.BookCode.ToUpper())
            .Select(bb => new
            {
                BibleId = bb.Bible.Id,
                BibleName = bb.Bible.Name,
                BibleAbbreviation = bb.Bible.Abbreviation,
                BookCode = bb.Code,
                BookName = bb.LocalizedName,
            })
            .FirstOrDefaultAsync(ct);

        if (bookData == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var greekAlignmentSource = await dbContext.Database
            .SqlQueryRaw<GreekAlignmentSourceResult>(
                GreekAlignmentSourceQuery,
                new SqlParameter("BibleId", request.BibleId))
            .FirstOrDefaultAsync(ct);

        if (greekAlignmentSource == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var baseQueryResults = await dbContext.Database
            .SqlQueryRaw<BaseQueryResult>(
                BaseQuery,
                new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds),
                new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        var greekWords = await dbContext.Database
            .SqlQueryRaw<GreekWords>(
                GreekWordsQuery,
                new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds),
                new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        var greekSenses = await dbContext.Database
            .SqlQueryRaw<GreekSenses>(
                GreekSensesQuery,
                new SqlParameter("LowerBounds", lowerBounds),
                new SqlParameter("UpperBounds", upperBounds),
                new SqlParameter("BibleId", request.BibleId))
            .ToListAsync(ct);

        var response = new Response
        {
            BibleId = bookData.BibleId,
            BibleName = bookData.BibleName,
            BibleAbbreviation = bookData.BibleAbbreviation,
            GreekBibleAbbreviation = greekAlignmentSource.Name,
            BookCode = bookData.BookCode,
            BookName = bookData.BookName,
            Chapters = baseQueryResults
                .GroupBy(r => r.BibleWordIdentifier.Chapter)
                .Select(c => new ResponseChapter
                {
                    Number = c.Key,
                    Verses = c
                        .GroupBy(r => r.BibleWordIdentifier.Verse)
                        .Select(v => new ResponseChapterVerse
                        {
                            Number = v.Key,
                            Words = v
                                .Select(w => new EnglishWordWithGreekAlignment
                                {
                                    Number = w.BibleWordIdentifier.Word,
                                    Word = w.EnglishWord,
                                    NextWordIsInGroup =
                                        v.OrderBy(next => next.WordIdentifier).SkipWhile(next => next.Id != w.Id).Skip(1).FirstOrDefault()
                                            ?.BibleVersionWordGroupId == w.BibleVersionWordGroupId && w.BibleVersionWordGroupId != null,
                                    GreekWords =
                                        w.BibleVersionWordGroupId == null ||
                                        v.OrderBy(next => next.WordIdentifier).First(next => next.BibleVersionWordGroupId == w.BibleVersionWordGroupId).Id == w.Id
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
                                                    .ToList(),
                                            })
                                            .ToList()
                                        : [],
                                })
                                .ToList(),
                        })
                        .ToList(),
                })
                .ToList(),
        };

        await SendOkAsync(response, ct);
    }

    public record GreekAlignmentSourceResult
    {
        public string Name { get; set; } = null!;
    }

    public record BaseQueryResult
    {
        public int Id { get; set; }
        public long WordIdentifier { get; set; }
        public string EnglishWord { get; set; } = null!;
        public int? BibleVersionWordGroupId { get; set; }

        public BibleWordIdentifier BibleWordIdentifier => _bibleWordIdentifier ??= new BibleWordIdentifier(WordIdentifier);

        private BibleWordIdentifier? _bibleWordIdentifier = null;
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