using System.Net;
using Aquifer.API.Endpoints.BibleBooks.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Bibles.Books.List;

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
        results.Should().HaveCountGreaterThan(80);
        results.Should().Contain(r => r.Code == "4MA");
    }
}