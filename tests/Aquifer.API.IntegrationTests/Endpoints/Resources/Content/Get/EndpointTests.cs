using System.Net;
using Aquifer.API.Endpoints.Resources.Content.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Resources.Content.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task UnauthenticatedRequest_ShouldReturnUnauthorized()
    {
        var (response, _) = await _app.Client.GETAsync<Endpoint, Request, ErrorResponse>(
            new Request
            {
                Id = 1890,
            });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AuthenticatedRequest_ShouldReturnOK()
    {
        var response = await _app.EditorClient.GETAsync<Endpoint, Request, Response>(
            new Request
            {
                Id = 1890,
            });

        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Result.ResourceContentId.Should().Be(1890);
    }
}