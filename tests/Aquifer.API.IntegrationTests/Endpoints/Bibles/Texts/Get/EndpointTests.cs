using System.Net;
using Aquifer.API.Endpoints.Bibles.Texts.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Bibles.Texts.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var request = new Request
        {
            BibleId = 1,
            BookCode = "MRK",
            StartChapter = 1,
            EndChapter = 1,
            StartVerse = 1,
            EndVerse = 1,
        };

        var (response, result) = await _app.AnonymousClient.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.BibleId.Should().Be(request.BibleId);
        result.BibleAbbreviation.Should().Be("BSB");

        result.BookCode.Should().Be(request.BookCode);
        result.BookNumber.Should().Be(42);
        result.BookName.Should().Be("Mark");

        result.Chapters.Should().HaveCount(1);
        var chapter = result.Chapters.First();
        chapter.Number.Should().Be(request.StartChapter);
        chapter.Verses.Should().HaveCount(1);

        var verse = chapter.Verses.First();
        verse.Number.Should().Be(request.StartVerse);
        verse.Text.Should().NotBeNullOrEmpty();
    }
}