using System.Data.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using Aquifer.Public.API.Helpers;
using Dapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
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
        var bookId = BibleBookCodeUtilities.IdFromCode(request.BookCode);

        var bookData = await GetBookDataAsync(request.BibleId, bookId, ct);

        if (bookData == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var dbConnection = dbContext.Database.GetDbConnection();

        var greekAlignmentNewTestamentName = await GetAssociatedGreekAlignmentNewTestamentNameForBibleAsync(
            dbConnection,
            request.BibleId,
            ct);

        if (greekAlignmentNewTestamentName == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var bibleText = await GetBibleTextAsync(
            dbConnection,
            request.BibleId,
            textLowerBounds: new BibleWordIdentifier(bookId, request.StartChapter, request.StartVerse, request.StartWord),
            textUpperBounds: BibleWordIdentifier.GetUpperBoundOfWord(bookId, request.EndChapter, request.EndVerse, request.EndWord),
            ct);

        var greekWordResultByIdMap = await GetGreekWordResultByGreekWordIdMapAsync(
            dbConnection,
            bibleText.Where(bt => bt.GreekWordId is not null).Select(r => r.GreekWordId!.Value),
            ct);

        var greekSenseResultsByIdMap = request.ShouldReturnSenseData
            ? await GetGreekSenseResultsByGreekSenseIdMapAsync(
                dbConnection,
                bibleText.Where(bt => bt.GreekSenseId is not null).Select(r => r.GreekSenseId!.Value),
                ct)
            : null;

        var response = new Response
        {
            BibleId = bookData.BibleId,
            BibleName = bookData.BibleName,
            BibleAbbreviation = bookData.BibleAbbreviation,
            GreekBibleAbbreviation = greekAlignmentNewTestamentName,
            BookCode = bookData.BookCode,
            BookName = bookData.BookName,
            Chapters = bibleText
                .GroupBy(r => r.BibleWordIdentifier.Chapter)
                .Select(wordsInChapter => new ResponseChapter
                {
                    Number = wordsInChapter.Key,
                    Verses = wordsInChapter
                        .GroupBy(r => r.BibleWordIdentifier.Verse)
                        .Select(wordsInVerse =>
                        {
                            var bibleVersionWordIdBookendsInGroupByGroupId = wordsInVerse
                                .GroupBy(r => r.BibleVersionWordGroupId)
                                .Where(grp => grp.Key is not null)
                                .ToDictionary(grp => grp.Key!.Value, grp => (First: grp.First().Id, Last: grp.Last().Id));

                            return new ResponseChapterVerse
                            {
                                Number = wordsInVerse.Key,
                                Words = wordsInVerse
                                    .GroupBy(r => r.BibleWordIdentifier.Word)
                                    .Select(greekWordsForWord =>
                                    {
                                        var bibleWord = greekWordsForWord.First();
                                        var (firstWordIdInGroup, lastWordIdInGroup) = bibleWord.BibleVersionWordGroupId is null
                                            ? (bibleWord.Id, bibleWord.Id)
                                            : bibleVersionWordIdBookendsInGroupByGroupId[bibleWord.BibleVersionWordGroupId!.Value];

                                        return MapToBibleWordResponse(
                                            wordNumber: bibleWord.BibleWordIdentifier.Word,
                                            bibleWord.Word,
                                            greekWordsForWord.Where(r => r.GreekWordId is not null).ToList(),
                                            isFirstWordInGroup: firstWordIdInGroup == bibleWord.Id,
                                            isLastWordInGroup: lastWordIdInGroup == bibleWord.Id,
                                            greekWordResultByIdMap,
                                            greekSenseResultsByIdMap);
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

    private static ResponseWordWithGreekAlignment MapToBibleWordResponse(
        int wordNumber,
        string word,
        IReadOnlyList<BibleTextWithGreekAlignmentForeignKeysResult> greekWordsForWord,
        bool isFirstWordInGroup,
        bool isLastWordInGroup,
        IReadOnlyDictionary<int, GreekWordResult> greekWordResultById,
        IReadOnlyDictionary<int, IReadOnlyList<GreekSenseResult>>? greekSenseResultsById)
    {
        return new ResponseWordWithGreekAlignment
        {
            Number = wordNumber,
            Word = word,
            NextWordIsInGroup = !isLastWordInGroup,
            GreekWords = isFirstWordInGroup
                ? greekWordsForWord
                    .GroupBy(r => r.GreekWordId!.Value)
                    .Select(greekSensesForGreekWord =>
                    {
                        var greekWordResult = greekWordResultById[greekSensesForGreekWord.Key];
                        var greekSenseResultsForGreekWord = greekSenseResultsById == null
                            ? null
                            : greekSensesForGreekWord
                                .Where(r => r.GreekSenseId is not null)
                                .SelectMany(r => greekSenseResultsById[r.GreekSenseId!.Value])
                                .ToList();

                        return MapToGreekWordResponse(greekWordResult, greekSenseResultsForGreekWord);
                    })
                    .ToList()
                : [],
        };
    }

    private static ResponseGreekWord MapToGreekWordResponse(
        GreekWordResult greekWordResult,
        IReadOnlyList<GreekSenseResult>? greekSenseResultsForGreekWord)
    {
        return new ResponseGreekWord
        {
            Word = greekWordResult.Word,
            GrammarType = greekWordResult.GrammarType,
            UsageCode = greekWordResult.UsageCode,
            Lemma = greekWordResult.Lemma,
            StrongsNumber = greekWordResult.StrongsNumber,
            Senses = greekSenseResultsForGreekWord
                ?.Select(gsr => new ResponseGreekSense
                {
                    Definition = gsr.Definition,
                    Glosses = gsr.ExpandedGlosses,
                })
                .ToList(),
        };
    }

    private async Task<BookData?> GetBookDataAsync(int bibleId, BookId bookId, CancellationToken ct)
    {
        return await dbContext.BibleBooks
            .Where(bb =>
                bb.Bible.Enabled &&
                !bb.Bible.RestrictedLicense &&
                bb.Bible.GreekAlignment &&
                bb.Bible.Id == bibleId &&
                bb.Code == BibleBookCodeUtilities.CodeFromId(bookId))
            .Select(bb => new BookData(
                bb.Bible.Id,
                bb.Bible.Name,
                bb.Bible.Abbreviation,
                bb.Code,
                bb.LocalizedName))
            .FirstOrDefaultAsync(ct);
    }

    private sealed record BookData(
        int BibleId,
        string BibleName,
        string BibleAbbreviation,
        string BookCode,
        string BookName);

    private static async Task<string?> GetAssociatedGreekAlignmentNewTestamentNameForBibleAsync(
        DbConnection dbConnection,
        int bibleId,
        CancellationToken ct)
    {
        const string greekAlignmentSourceQuery = """
            SELECT TOP 1 gnt.[Name]
            FROM BibleVersionWords bvw
                LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
                LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = bvwgw.BibleVersionWordGroupId
                LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = nta.GreekNewTestamentWordGroupId
                LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
                LEFT JOIN GreekNewTestaments gnt ON gntw.GreekNewTestamentId = gnt.Id
            WHERE
                bvw.BibleId = @bibleId AND
                gnt.Id IS NOT NULL
            """;

        return await dbConnection.QueryFirstOrDefaultAsync<string>(
            new CommandDefinition(
                greekAlignmentSourceQuery,
                new
                {
                    bibleId
                },
                cancellationToken: ct));
    }

    private static async Task<IReadOnlyList<BibleTextWithGreekAlignmentForeignKeysResult>> GetBibleTextAsync(
        DbConnection dbConnection,
        int bibleId,
        BibleWordIdentifier textLowerBounds,
        BibleWordIdentifier textUpperBounds,
        CancellationToken ct)
    {
        // Fetches all Bible text in the range for the given Bible.
        // Includes foreign key links to Greek Word and Greek Sense for later lookup.
        // Note that if there is more than one Greek sense for a Greek word then there will be multiple rows returned, one for each sense.
        const string bibleTextWithGreekAlignmentForeignKeysQuery = """
            SELECT
                bvw.Id,
                bvw.WordIdentifier,
                bvw.[Text] AS Word,
                bvw.IsPunctuation,
                bvwgw.BibleVersionWordGroupId,
                gntws.GreekSenseId,
                gntw.GreekWordId
            FROM BibleVersionWords bvw
                LEFT JOIN BibleVersionWordGroupWords bvwgw ON bvwgw.BibleVersionWordId = bvw.Id
                LEFT JOIN BibleVersionWordGroups bvwg ON bvwg.Id = bvwgw.BibleVersionWordGroupId
                LEFT JOIN NewTestamentAlignments nta ON nta.BibleVersionWordGroupId = bvwg.Id
                LEFT JOIN GreekNewTestamentWordGroups gntwg ON gntwg.Id = nta.GreekNewTestamentWordGroupId
                LEFT JOIN GreekNewTestamentWordGroupWords gntwgw ON gntwgw.GreekNewTestamentWordGroupId = gntwg.Id
                LEFT JOIN GreekNewTestamentWords gntw ON gntw.Id = gntwgw.GreekNewTestamentWordId
                LEFT JOIN GreekNewTestamentWordSenses gntws ON gntws.GreekNewTestamentWordId = gntw.Id
            WHERE
                bvw.BibleId = @bibleId AND
                bvw.WordIdentifier BETWEEN @lowerBounds AND @upperBounds
            ORDER BY
                bvw.WordIdentifier,
                gntw.WordIdentifier,
                gntws.GreekSenseId
            """;

        return (await dbConnection.QueryAsync<BibleTextWithGreekAlignmentForeignKeysResult>(
                new CommandDefinition(
                    bibleTextWithGreekAlignmentForeignKeysQuery,
                    new
                    {
                        bibleId,
                        lowerBounds = textLowerBounds.WordIdentifier,
                        upperBounds = textUpperBounds.WordIdentifier,
                    },
                    cancellationToken: ct)))
            .ToList();
    }

    public sealed record BibleTextWithGreekAlignmentForeignKeysResult(
        int Id,
        long WordIdentifier,
        string Word,
        bool IsPunctuation,
        int? BibleVersionWordGroupId,
        int? GreekSenseId,
        int? GreekWordId)
    {
        public BibleWordIdentifier BibleWordIdentifier => _bibleWordIdentifier ??= new BibleWordIdentifier(WordIdentifier);

        private BibleWordIdentifier? _bibleWordIdentifier;
    }

    private static async Task<IReadOnlyDictionary<int, GreekWordResult>> GetGreekWordResultByGreekWordIdMapAsync(
        DbConnection dbConnection,
        IEnumerable<int> greekWordIds,
        CancellationToken ct)
    {
        const string greekWordsQuery = """
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

        List<GreekWordResult> greekWordResults = [];
        foreach (var batch in greekWordIds.Distinct().Order().Chunk(size: SqlParameterBatchSize))
        {
            greekWordResults.AddRange(
                await dbConnection.QueryAsync<GreekWordResult>(
                    new CommandDefinition(
                        greekWordsQuery,
                        new
                        {
                            greekWordIds = batch
                        },
                        cancellationToken: ct)));
        }

        return greekWordResults
            .ToDictionary(gwr => gwr.Id);
    }

    public sealed record GreekWordResult(
        int Id,
        string Word,
        string GrammarType,
        string UsageCode,
        string Lemma,
        string StrongsNumber);

    private static async Task<IReadOnlyDictionary<int, IReadOnlyList<GreekSenseResult>>> GetGreekSenseResultsByGreekSenseIdMapAsync(
        DbConnection dbConnection,
        IEnumerable<int> greekSenseIds,
        CancellationToken ct)
    {
        const string greekSensesQuery = """
            SELECT
                gs.Id,
                gs.DefinitionShort AS [Definition],
                STRING_AGG(gsg.[Text], '||') AS Glosses
            FROM GreekSenses gs
                LEFT JOIN GreekSenseGlosses gsg ON gsg.GreekSenseId = gs.Id
            WHERE gs.Id IN @greekSenseIds
            GROUP BY
                gs.Id,
                gs.DefinitionShort
            """;

        List<GreekSenseResult> greekSenseResults = [];
        foreach (var batch in greekSenseIds.Distinct().Order().Chunk(size: SqlParameterBatchSize))
        {
            greekSenseResults.AddRange(
                await dbConnection.QueryAsync<GreekSenseResult>(
                    new CommandDefinition(
                        greekSensesQuery,
                        new
                        {
                            greekSenseIds = batch
                        },
                        cancellationToken: ct)));
        }

        return new Dictionary<int, IReadOnlyList<GreekSenseResult>>(
            greekSenseResults
                .GroupBy(gsr => gsr.Id)
                .Select(grp => new KeyValuePair<int, IReadOnlyList<GreekSenseResult>>(grp.Key, [.. grp])));
    }

    public sealed record GreekSenseResult(
        int Id,
        string Definition,
        string? Glosses)
    {
        public IReadOnlyList<string> ExpandedGlosses => _expandedGlosses ??= Glosses?.Split("||").Order().ToList() ?? [];

        private IReadOnlyList<string>? _expandedGlosses;
    }

    // SQL Server's max is 2,100 but batching in smaller numbers seems to help with DB performance
    private const int SqlParameterBatchSize = 1000;
}