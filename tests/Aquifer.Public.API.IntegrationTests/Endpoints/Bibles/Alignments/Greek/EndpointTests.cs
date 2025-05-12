using System.Net;
using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.Alignments.Greek;

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
            StartWord = 2,
            EndChapter = 3,
            EndVerse = 2,
            EndWord = 3,
            ShouldReturnSenseData = true,
        },
        new Request
        {
            BibleId = 2,
            StartChapter = 2,
            EndChapter = 2,
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMRK),
            ShouldReturnSenseData = false,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.AnonymousClient.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        // assert that Bible and book data are populated and match the request
        result.BibleId.Should().Be(request.BibleId);
        result.BibleAbbreviation.Should().NotBeNullOrEmpty();
        result.BibleName.Should().NotBeNullOrEmpty();
        result.BookCode.Should().Be(request.BookCode);
        result.BookName.Should().NotBeNullOrEmpty();
        result.GreekBibleAbbreviation.Should().NotBeNullOrEmpty();

        // assert that the chapters, verses, and words values match the request
        result.Chapters.Should().NotBeNullOrEmpty();
        var orderedChapters = result.Chapters.OrderBy(c => c.Number).ToList();
        orderedChapters.Count.Should().Be(request.EndChapter - request.StartChapter + 1);

        var firstChapter = orderedChapters.First();
        var firstVerse = firstChapter.Verses.OrderBy(v => v.Number).First();
        var firstWord = firstVerse.Words.OrderBy(w => w.Number).First();

        firstChapter.Number.Should().Be(request.StartChapter > 0 ? request.StartChapter : 1);
        firstVerse.Number.Should().Be(request.StartVerse > 0 ? request.StartVerse : 1);
        firstWord.Number.Should().Be(request.StartWord > 0 ? request.StartWord : 1);

        var lastChapter = orderedChapters.Last();
        var lastVerse = lastChapter.Verses.OrderByDescending(v => v.Number).First();
        var lastWord = lastVerse.Words.OrderByDescending(w => w.Number).First();

        if (request.EndChapter < 999)
        {
            lastChapter.Number.Should().Be(request.EndChapter);
        }

        if (request.EndVerse < 999)
        {
            lastVerse.Number.Should().Be(request.EndVerse);
        }

        if (request.EndWord < 999)
        {
            lastWord.Number.Should().Be(request.EndWord);
        }

        // assert that all chapter/verse/word data is populated
        foreach (var chapter in orderedChapters)
        {
            chapter.Number.Should().BeGreaterThan(0);
            chapter.Verses.Should().NotBeNullOrEmpty();

            foreach (var verse in chapter.Verses)
            {
                verse.Number.Should().BeGreaterThan(0);
                verse.Words.Should().NotBeNullOrEmpty();

                foreach (var word in verse.Words)
                {
                    word.Number.Should().BeGreaterThan(0);
                    word.Word.Should().NotBeNullOrEmpty();
                    foreach (var greekWord in word.GreekWords)
                    {
                        greekWord.GrammarType.Should().NotBeNullOrEmpty();
                        greekWord.Lemma.Should().NotBeNullOrEmpty();
                        greekWord.StrongsNumber.Should().NotBeNullOrEmpty();
                        greekWord.UsageCode.Should().NotBeNullOrEmpty();
                        greekWord.Word.Should().NotBeNullOrEmpty();

                        if (!request.ShouldReturnSenseData)
                        {
                            greekWord.Senses.Should().BeNull();
                        }
                        else
                        {
                            greekWord.Senses.Should().NotBeNull();
                            foreach (var sense in greekWord.Senses!)
                            {
                                sense.Definition.Should().NotBeNull();
                                foreach (var gloss in sense.Glosses)
                                {
                                    gloss.Should().NotBeNullOrEmpty();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}