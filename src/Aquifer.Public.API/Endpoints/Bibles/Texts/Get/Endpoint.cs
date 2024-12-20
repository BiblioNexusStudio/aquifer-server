using Aquifer.Common.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext, IBibleTextService bibleTextService)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        Description(d => d
            .WithTags("Bibles")
            .ProducesProblemFE()
            .ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "Get the Bible text contained within a book of the Bible.";
            s.Description =
                "For a given Bible and book of the Bible, returns the Bible text (and optional audio information if available) for all verses within the chapter and verse parameters.";
        });
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var bookData = await dbContext.BibleBookContents
            .Where(bbc =>
                bbc.Bible.Enabled &&
                !bbc.Bible.RestrictedLicense &&
                bbc.Bible.Id == request.BibleId &&
                bbc.Book.Code == request.BookCode.ToUpper())
            .Select(bbc => new IntermediateBookData
            {
                BibleId = bbc.Bible.Id,
                BibleName = bbc.Bible.Name,
                BibleAbbreviation = bbc.Bible.Abbreviation,
                BookCode = bbc.Book.Code,
                BookId = bbc.BookId,
                BookName = bbc.DisplayName,
                AudioUrls = bbc.AudioUrls
            })
            .FirstOrDefaultAsync(ct);

        var bookCode = (int)BibleBookCodeUtilities.IdFromCode(request.BookCode);

        var bibleTexts = await bibleTextService.GetBibleTextsForBibleId(request.BibleId,
            BibleUtilities.GetVerseId(bookCode, request.StartChapter, request.StartVerse),
            BibleUtilities.GetVerseId(bookCode, request.EndChapter, request.EndVerse), ct);

        var response = bookData is null
            ? null
            : new Response
            {
                BibleId = bookData.BibleId,
                BibleName = bookData.BibleName,
                BibleAbbreviation = bookData.BibleAbbreviation,
                BookCode = bookData.BookCode,
                BookName = bookData.BookName,
                Chapters = bibleTexts.Select(bt =>
                {
                    var chapterAudio = !request.ShouldReturnAudioData
                        ? null
                        : DeserializeAudioUrls(bookData.AudioUrls)?.Chapters?.FirstOrDefault(c => c.Number == bt.ChapterNumber.ToString());
                    return new ResponseChapter
                    {
                        Number = bt.ChapterNumber,
                        Audio = MapToResponseChapterAudio(chapterAudio),
                        Verses = bt.Verses.Select(v => new ResponseChapterVerse
                        {
                            Number = v.VerseNumber,
                            AudioTimestamp = MapToResponseAudioTimestamp(
                                chapterAudio?.AudioTimestamps?.FirstOrDefault(at => at.VerseNumber == v.VerseNumber.ToString())),
                            Text = v.Text
                        }).ToList()
                    };
                }).ToList()
            };

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }

    private static BibleBookContentEntity.AudioUrlsData? DeserializeAudioUrls(string? json)
    {
        return json is null ? null : JsonUtilities.DefaultDeserialize<BibleBookContentEntity.AudioUrlsData>(json);
    }

    private static ResponseChapterAudio? MapToResponseChapterAudio(BibleBookContentEntity.AudioUrlsData.Chapter? audioChapter)
    {
        return audioChapter == null
            ? null
            : new ResponseChapterAudio
            {
                Webm = MapToResponseAudioFile(audioChapter.Webm),
                Mp3 = MapToResponseAudioFile(audioChapter.Mp3)
            };
    }

    private static ResponseAudioFile? MapToResponseAudioFile(BibleBookContentEntity.AudioUrlsData.Chapter.AudioUrl? audioUrl)
    {
        return audioUrl == null
            ? null
            : new ResponseAudioFile
            {
                Url = audioUrl.Url,
                Size = audioUrl.Size
            };
    }

    private static ResponseChapterVerseAudioTimestamp? MapToResponseAudioTimestamp(
        BibleBookContentEntity.AudioUrlsData.Chapter.AudioTimestamp? audioTimestamp)
    {
        return audioTimestamp == null
            ? null
            : new ResponseChapterVerseAudioTimestamp
            {
                Start = audioTimestamp.Start,
                End = audioTimestamp.End
            };
    }

    private class IntermediateBookData
    {
        public required int BibleId { get; set; }
        public required BookId BookId { get; set; }
        public required string BibleName { get; set; }
        public required string BibleAbbreviation { get; set; }
        public required string BookCode { get; set; }
        public required string BookName { get; set; }
        public required string? AudioUrls { get; set; }
        public List<IntermediateChapter>? Chapters { get; set; }
    }

    private class IntermediateChapter
    {
        public required int Number { get; set; }
        public required List<IntermediateChapterVerse> Verses { get; set; }
    }

    private class IntermediateChapterVerse
    {
        public required int Number { get; set; }
        public required string Text { get; set; }
    }
}