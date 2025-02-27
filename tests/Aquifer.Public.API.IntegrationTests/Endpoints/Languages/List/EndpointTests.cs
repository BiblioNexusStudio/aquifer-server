using System.Net;
using Aquifer.Public.API.Endpoints.Languages.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Languages.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();

        results.Count.Should().BeGreaterThan(0);
        foreach (var result in results)
        {
            result.Id.Should().BeGreaterThan(0);
            result.Code.Should().NotBeNullOrEmpty();
            result.EnglishDisplay.Should().NotBeNullOrEmpty();
            result.LocalizedDisplay.Should().NotBeNull();
            result.ScriptDirection.Should().NotBeNullOrEmpty();
        }

        results.Should().Contain(x => x.Code == "fra" && x.EnglishDisplay == "French" && x.LocalizedDisplay == "Français");
    }
}