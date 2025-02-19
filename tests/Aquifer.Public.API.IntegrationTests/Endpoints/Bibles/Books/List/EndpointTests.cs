using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.Books.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.Books.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        result.Count.Should().BeGreaterThan(0);
        foreach (var book in result)
        {
            book.Name.Should().NotBeNullOrEmpty();
            book.Code.Should().NotBeNullOrEmpty();
        }

        result.Should().Contain(x => x.Name == "Genesis" && x.Code == "GEN");
    }
}