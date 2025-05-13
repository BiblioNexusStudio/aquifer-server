using System.Net;
using Aquifer.API.Endpoints.Bibles.Texts.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Bibles.Texts.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            BibleId = 1,
            BookCode = "MRK",
            StartChapter = 1,
            EndChapter = 1,
            StartVerse = 1,
            EndVerse = 1,
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