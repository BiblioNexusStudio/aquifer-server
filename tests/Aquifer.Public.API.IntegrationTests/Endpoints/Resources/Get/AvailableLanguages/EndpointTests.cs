using System.Net;
using Aquifer.Public.API.Endpoints.Resources.Get.AvailableLanguages;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Get.AvailableLanguages;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    private const int ImageResourceContentId = 1372;
    private const int TextResourceContentId = 1438;

    [Theory]
    [InlineData(ImageResourceContentId, "tpi")]
    [InlineData(TextResourceContentId, "hin")]
    public async Task ValidRequest_ShouldReturnSuccess(int resourceContentId, string expectedLanguageCode)
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(
            new Request
            {
                ContentId = resourceContentId,
                
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();

        foreach (var result in results)
        {
            // assert that all response data is populated
            result.ContentId.Should().BeGreaterThan(0);
            result.ContentDisplayName.Should().NotBeNullOrEmpty();
            result.LanguageCode.Should().NotBeNullOrEmpty();
            result.LanguageDisplayName.Should().NotBeNull();
            result.LanguageEnglishDisplayName.Should().NotBeNullOrEmpty();
            result.LanguageId.Should().BeGreaterThan(0);
        }

        // assert expected language code is present
        results.Should().Contain(al => al.LanguageCode == expectedLanguageCode);
    }
}