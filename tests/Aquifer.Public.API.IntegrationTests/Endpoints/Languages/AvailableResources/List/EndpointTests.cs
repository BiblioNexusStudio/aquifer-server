using System.Net;
using Aquifer.Common.Utilities;
using Aquifer.Data.Enums;
using Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Languages.AvailableResources.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMRK),
            LanguageCodes = ["eng", "fra"],
        },
        new Request
        {
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMAT),
            StartChapter = 2,
            StartVerse = 3,
            EndChapter = 5,
            EndVerse = 7,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();

        // assert that response data matches the request
        if (request.LanguageCodes.Length > 0)
        {
            results.Count.Should().Be(request.LanguageCodes.Length);
            foreach (var languageCode in request.LanguageCodes)
            {
                results.Should().ContainSingle(x => x.LanguageCode == languageCode);
            }
        }

        // assert that response data is populated
        foreach (var result in results)
        {
            result.LanguageId.Should().BeGreaterThan(0);
            result.LanguageCode.Should().NotBeNullOrEmpty();

            result.ResourceCounts.Should().NotBeNullOrEmpty();
            foreach (var resourceCount in result.ResourceCounts)
            {
                resourceCount.Count.Should().BeGreaterOrEqualTo(0);
                resourceCount.Type.Should().NotBeNullOrEmpty();
            }
        }
    }
}