using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        Description(d => d.ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Gets the Bible text contained within a book of the Bible.";
            s.Description =
                "For a given Bible and book of the Bible, returns the Bible text (and optional audio information if available) for all verses within the chapter and verse parameters.";
        });
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var startChapter = request.StartChapter ?? 1;
        var endChapter = request.EndChapter ?? 999;
        var startVerse = request.StartVerse ?? 1;
        var endVerse = request.EndVerse ?? 999;

        var bookData = await dbContext.BibleBooks
            .Where(bb => bb.Bible.Enabled && bb.Bible.Id == request.BibleId && bb.Code == request.BookCode!.ToUpper())
            .Select(bb => new
            {
                BibleId = bb.Bible.Id,
                BibleName = bb.Bible.Name,
                BibleAbbreviation = bb.Bible.Abbreviation,
                BookCode = bb.Code,
                BookId = bb.Number,
                BookName = bb.LocalizedName,
                Chapters = bb.Chapters
                    .Where(ch => ch.Number >= startChapter && ch.Number <= endChapter)
                    .Select(ch => new
                    {
                        ch.Number,
                        Verses = ch.Verses
                            .Where(v => (ch.Number != startChapter || v.Number >= startVerse) &&
                                (ch.Number != endChapter || v.Number <= endVerse))
                            .ToList(),
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync(ct);

        var parsedAudioUrls = bookData is null || !request.ShouldReturnAudioData
            ? null
            : DeserializeAudioUrls(
                await dbContext.BibleBookContents
                    .Where(bbc => bbc.BibleId == request.BibleId && bbc.BookId == bookData.BookId)
                    .Select(bbc => bbc.AudioUrls)
                    .FirstOrDefaultAsync(ct));

        var response = bookData is null
            ? null
            : new Response
            {
                BibleId = bookData.BibleId,
                BibleName = bookData.BibleName,
                BibleAbbreviation = bookData.BibleAbbreviation,
                BookCode = bookData.BookCode,
                BookName = bookData.BookName,
                Chapters = bookData.Chapters
                    .Select(ch =>
                    {
                        var chapterAudio = parsedAudioUrls?.Chapters?.FirstOrDefault(c => c.Number == ch.Number.ToString());

                        return new ResponseChapter
                        {
                            Number = ch.Number,
                            Audio = MapToResponseChapterAudio(chapterAudio),
                            Verses = ch.Verses
                                .Select(v => new ResponseChapterVerse
                                {
                                    Number = v.Number,
                                    AudioTimestamp = MapToResponseAudioTimestamp(chapterAudio?.AudioTimestamps?.FirstOrDefault(at => at.VerseNumber == v.Number.ToString())),
                                    Text = v.Text,
                                })
                                .ToList(),
                        };
                    })
                    .ToList(),
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
                Mp3 = MapToResponseAudioFile(audioChapter.Mp3),
            };
    }

    private static ResponseAudioFile? MapToResponseAudioFile(BibleBookContentEntity.AudioUrlsData.Chapter.AudioUrl? audioUrl)
    {
        return audioUrl == null
            ? null
            : new ResponseAudioFile
            {
                Url = audioUrl.Url,
                Size = audioUrl.Size,
            };
    }
    private static ResponseChapterVerseAudioTimestamp? MapToResponseAudioTimestamp(BibleBookContentEntity.AudioUrlsData.Chapter.AudioTimestamp? audioTimestamp)
    {
        return audioTimestamp == null
            ? null
            : new ResponseChapterVerseAudioTimestamp
            {
                Start = audioTimestamp.Start,
                End = audioTimestamp.End,
            };
    }
}