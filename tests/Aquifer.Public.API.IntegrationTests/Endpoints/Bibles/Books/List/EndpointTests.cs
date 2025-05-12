using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.Books.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.Books.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized()
    {
        var (response, results) = await _app.Client.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        results.Should().BeNull();
    }

    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNull();

        results.Count.Should().BeGreaterThan(0);
        foreach (var result in results)
        {
            result.Name.Should().NotBeNullOrEmpty();
            result.Code.Should().NotBeNullOrEmpty();
        }

        results.Should().Contain(x => x.Name == "Genesis" && x.Code == "GEN");
    }
}