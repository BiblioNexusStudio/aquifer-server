using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.List;
using FastEndpoints.Testing;
using FastEndpoints;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(
            new Request
            {
                IsLanguageDefault = true,
                LanguageId = 1,
                HasAudio = true,
                HasGreekAlignment = true,
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().Contain(r => r.Abbreviation == "BSB");
    }
}