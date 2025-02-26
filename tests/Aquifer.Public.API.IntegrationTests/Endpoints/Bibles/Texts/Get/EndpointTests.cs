using System.Net;
using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using Aquifer.Public.API.Endpoints.Bibles.Texts.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.Texts.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            BibleId = 1,
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMAT),
            StartChapter = 2,
            StartVerse = 3,
            EndChapter = 3,
            EndVerse = 2,
            ShouldReturnAudioData = true,
        },
        new Request
        {
            BibleId = 2,
            StartChapter = 2,
            EndChapter = 2,
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMRK),
            ShouldReturnAudioData = false,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        // assert that Bible and book data are populated and match the request
        result.BibleId.Should().Be(request.BibleId);
        result.BibleAbbreviation.Should().NotBeNullOrEmpty();
        result.BibleName.Should().NotBeNullOrEmpty();
        result.BookCode.Should().Be(request.BookCode);
        result.BookName.Should().NotBeNullOrEmpty();

        // assert that the chapters, verses, and words values match the request
        result.Chapters.Should().NotBeNullOrEmpty();
        var orderedChapters = result.Chapters.OrderBy(c => c.Number).ToList();
        orderedChapters.Count.Should().Be(request.EndChapter - request.StartChapter + 1);

        var firstChapter = orderedChapters.First();
        var firstVerse = firstChapter.Verses.OrderBy(v => v.Number).First();

        firstChapter.Number.Should().Be(request.StartChapter > 0 ? request.StartChapter : 1);
        firstVerse.Number.Should().Be(request.StartVerse > 0 ? request.StartVerse : 1);

        var lastChapter = orderedChapters.Last();
        var lastVerse = lastChapter.Verses.OrderByDescending(v => v.Number).First();

        if (request.EndChapter < 999)
        {
            lastChapter.Number.Should().Be(request.EndChapter);
        }

        if (request.EndVerse < 999)
        {
            lastVerse.Number.Should().Be(request.EndVerse);
        }

        // assert that all chapter/verse data is populated
        foreach (var chapter in orderedChapters)
        {
            chapter.Number.Should().BeGreaterThan(0);
            chapter.Verses.Should().NotBeEmpty();

            if (!request.ShouldReturnAudioData)
            {
                chapter.Audio.Should().BeNull();
            }
            else
            {
                chapter.Audio.Should().NotBeNull();

                if (chapter.Audio!.Mp3 != null)
                {
                    chapter.Audio.Mp3.Size.Should().BeGreaterThan(0);
                    chapter.Audio.Mp3.Url.Should().NotBeNullOrEmpty();
                }

                if (chapter.Audio.Webm != null)
                {
                    chapter.Audio.Webm.Size.Should().BeGreaterThan(0);
                    chapter.Audio.Webm.Url.Should().NotBeNullOrEmpty();
                }
            }

            foreach (var verse in chapter.Verses)
            {
                verse.Number.Should().BeGreaterThan(0);
                verse.Text.Should().NotBeNullOrEmpty();

                if (!request.ShouldReturnAudioData)
                {
                    verse.AudioTimestamp.Should().BeNull();
                }
                else
                {
                    verse.AudioTimestamp.Should().NotBeNull();
                    verse.AudioTimestamp!.End.Should().BeGreaterThan(verse.AudioTimestamp.Start);
                }
            }
        }
    }
}