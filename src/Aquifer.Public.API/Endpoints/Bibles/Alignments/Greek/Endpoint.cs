using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Public.API.Helpers;
using Dapper;
using FastEndpoints;
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
        WHERE bvw.BibleId = @bibleId
        """;

    // Fetches all Bible text in the range for the given Bible.
    // Includes foreign key links to Greek Word and Greek Sense for later lookup.
    // Note that if there is more than one Greek sense for a Greek word then there will be multiple rows returned, one for each sense.
    private const string BibleTextWithGreekAlignmentForeignKeysQuery = """
        SELECT
            bvw.Id,
            bvw.WordIdentifier,
            bvw.[Text] AS EnglishWord,
            bvw.IsPunctuation,
            bvwgw.BibleVersionWordGroupId,
            gntws.GreekSenseId,
            gntw.GreekWordId
        FROM BibleVersionWords bvw
            LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
            LEFT JOIN BibleVersionWordGroups bvwg ON bvwg.Id = bvwgw.BibleVersionWordGroupId
            LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = BVWG.Id
            LEFT JOIN GreekNewTestamentWordGroups gntwg ON gntwg.Id = nta.GreekNewTestamentWordGroupId
            LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = GNTWG.Id
            LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
            LEFT JOIN GreekNewTestamentWordSenses gntws ON gntws.GreekNewTestamentWordId = gntw.Id
        WHERE
            bvw.BibleId = @bibleId AND
            bvw.WordIdentifier BETWEEN @lowerBounds AND @upperBounds AND
            gntw.Id IS NOT NULL
        ORDER BY
            bvw.WordIdentifier,
            gntw.WordIdentifier,
            gntws.GreekSenseId
        """;

    private const string GreekWordsQuery = """
        SELECT
            gw.Id,
            gw.[Text] AS Word,
            gw.GrammarType,
            gw.UsageCode,
            gl.[Text] AS Lemma,
            sn.[Value] AS StrongsNumber
        FROM GreekWords gw
            JOIN GreekLemmas gl ON gw.GreekLemmaId = gl.Id
            JOIN StrongNumbers sn ON gl.StrongNumberId = sn.Id
        WHERE gw.Id IN @greekWordIds
        """;

    private const string GreekSensesQuery = """
        SELECT
            gs.Id,
            gs.DefinitionShort AS [Definition],
            STRING_AGG(gsg.[Text], '||') AS Glosses
        FROM GreekSenses gs
            JOIN GreekSenseGlosses gsg ON gsg.GreekSenseId = gs.Id
        WHERE gs.Id IN @greekSenseIds
        GROUP BY
            gs.Id,
            gs.DefinitionShort
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

        var greekAlignmentSource = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>(new CommandDefinition(GreekAlignmentSourceQuery, new { bibleId = request.BibleId }, cancellationToken: ct));

        if (greekAlignmentSource == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var bibleText = (await dbContext.Database.GetDbConnection()
                .QueryAsync<BibleTextWithGreekAlignmentForeignKeysResult>(
                    new CommandDefinition(
                        BibleTextWithGreekAlignmentForeignKeysQuery,
                        new
                        {
                            lowerBounds,
                            upperBounds,
                            bibleId = request.BibleId,
                        },
                        cancellationToken: ct)))
            .ToList();

        List<GreekWordResult> greekWordResults = [];
        foreach (var batch in bibleText.Select(r => r.GreekWordId).Distinct().Order().Chunk(size: 2000))
        {
            greekWordResults.AddRange(await dbContext.Database.GetDbConnection()
                .QueryAsync<GreekWordResult>(new CommandDefinition(GreekWordsQuery, new { greekWordIds = batch }, cancellationToken: ct)));
        }

        var greekWordResultById = greekWordResults.ToDictionary(gwr => gwr.Id);

        List<GreekSenseResult> greekSenseResults = [];
        if (request.ShouldReturnSenseData)
        {
            foreach (var batch in bibleText.Select(r => r.GreekSenseId).Distinct().Order().Chunk(size: 2000))
            {
                greekSenseResults.AddRange(await dbContext.Database.GetDbConnection()
                    .QueryAsync<GreekSenseResult>(new CommandDefinition(GreekSensesQuery, new { greekSenseIds = batch }, cancellationToken: ct)));
            }
        }

        var greekSenseResultById = greekSenseResults.ToLookup(r => r.Id);

        var response = new Response
        {
            BibleId = bookData.BibleId,
            BibleName = bookData.BibleName,
            BibleAbbreviation = bookData.BibleAbbreviation,
            GreekBibleAbbreviation = greekAlignmentSource,
            BookCode = bookData.BookCode,
            BookName = bookData.BookName,
            Chapters = bibleText
                .GroupBy(r => r.BibleWordIdentifier.Chapter)
                .Select(chapterGrouping => new ResponseChapter
                {
                    Number = chapterGrouping.Key,
                    Verses = chapterGrouping
                        .GroupBy(r => r.BibleWordIdentifier.Verse)
                        .Select(verseGrouping =>
                        {
                            var bibleVersionWordIdBookendsInBibleVersionWordGroupByBibleVersionWordGroupId = verseGrouping
                                .GroupBy(r => r.BibleVersionWordGroupId)
                                .ToDictionary(grp => grp.Key, grp => (First: grp.First().Id, Last: grp.Last().Id));

                            return new ResponseChapterVerse
                            {
                                Number = verseGrouping.Key,
                                Words = verseGrouping
                                    .GroupBy(r => r.BibleWordIdentifier.Word)
                                    .Select(wordGrouping =>
                                    {
                                        var word = wordGrouping.First();
                                        return new EnglishWordWithGreekAlignment
                                        {
                                            Number = word.BibleWordIdentifier.Word,
                                            Word = word.EnglishWord,
                                            NextWordIsInGroup = bibleVersionWordIdBookendsInBibleVersionWordGroupByBibleVersionWordGroupId[word.BibleVersionWordGroupId].Last != word.Id,
                                            GreekWords =
                                                bibleVersionWordIdBookendsInBibleVersionWordGroupByBibleVersionWordGroupId[word.BibleVersionWordGroupId].First == word.Id
                                                ? wordGrouping
                                                    .GroupBy(r => r.GreekWordId)
                                                    .Select(greekWordGrouping =>
                                                    {
                                                        var greekWordResult = greekWordResultById[greekWordGrouping.Key];
                                                        var greekSenseResults = greekWordGrouping
                                                            .SelectMany(r => greekSenseResultById[r.GreekSenseId])
                                                            .ToList();

                                                        return new GreekWord
                                                        {
                                                            Word = greekWordResult.Word,
                                                            GrammarType = greekWordResult.GrammarType,
                                                            UsageCode = greekWordResult.UsageCode,
                                                            Lemma = greekWordResult.Lemma,
                                                            StrongsNumber = greekWordResult.StrongsNumber,
                                                            Senses = !request.ShouldReturnSenseData
                                                                ? null
                                                                : greekSenseResults
                                                                    .OrderBy(s => s.Definition)
                                                                    .Select(gsr => new GreekSense
                                                                    {
                                                                        Definition = gsr.Definition,
                                                                        Glosses = gsr.Glosses?.Split("||").Order().ToList() ?? []
                                                                    })
                                                                    .ToList(),
                                                        };
                                                    })
                                                    .ToList()
                                                : [],
                                        };
                                    })
                                    .ToList(),
                            };
                        })
                        .ToList(),
                })
                .ToList(),
        };

        await SendOkAsync(response, ct);
    }

    public record BibleTextWithGreekAlignmentForeignKeysResult
    {
        public int Id { get; set; }
        public long WordIdentifier { get; set; }
        public string EnglishWord { get; set; } = null!;
        public bool IsPunctuation { get; set; }
        public int BibleVersionWordGroupId { get; set; }
        public int GreekSenseId { get; set; }
        public int GreekWordId { get; set; }

        public BibleWordIdentifier BibleWordIdentifier => _bibleWordIdentifier ??= new BibleWordIdentifier(WordIdentifier);

        private BibleWordIdentifier? _bibleWordIdentifier = null;
    }

    public record GreekWordResult
    {
        public int Id { get; set; }
        public string Word { get; set; } = null!;
        public string GrammarType { get; set; } = null!;
        public string UsageCode { get; set; } = null!;
        public string Lemma { get; set; } = null!;
        public string StrongsNumber { get; set; } = null!;
    }

    public record GreekSenseResult
    {
        public int Id { get; set; }
        public string Definition { get; set; } = null!;
        public string? Glosses { get; set; }
    }
}