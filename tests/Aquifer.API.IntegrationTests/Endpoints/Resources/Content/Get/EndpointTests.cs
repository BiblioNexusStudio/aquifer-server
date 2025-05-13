using System.Net;
using Aquifer.API.Endpoints.Resources.Content.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Resources.Content.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    private const int ResourceContentId = 1890;

    [Fact]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized()
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(
            new Request
            {
                Id = ResourceContentId,
            });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.Should().BeNull();
    }

    [Fact]
    public async Task InvalidRequest_UnauthenticatedRequest_ShouldReturnUnauthorized()
    {
        var (response, result) = await _app.AnonymousClient.GETAsync<Endpoint, Request, Response>(
            new Request
            {
                Id = ResourceContentId,
            });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticatedRequest_ShouldReturnOK()
    {
        var response = await _app.EditorClient.GETAsync<Endpoint, Request, Response>(
            new Request
            {
                Id = ResourceContentId,
            });

        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Result.ResourceContentId.Should().Be(1890);
    }
}