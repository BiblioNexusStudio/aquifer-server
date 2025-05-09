using System.Net;
using Aquifer.API.Endpoints.BibleBooks.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Bibles.Books.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _app.AnonymousClient.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().HaveCountGreaterThan(80);
        result.Should().Contain(r => r.Code == "4MA");
    }
}