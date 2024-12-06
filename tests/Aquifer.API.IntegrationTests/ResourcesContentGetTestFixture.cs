using System.Net;
using Aquifer.API.Endpoints.Resources.Content.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests;

public sealed class ResourcesContentGetTestFixture(AppFixture _appFixture) : TestBase<AppFixture>
{
    [Fact]
    public async Task UnauthenticatedRequest_ShouldReturnUnauthorized()
    {
        var (response, _) = await _appFixture.Client.GETAsync<Endpoint, Request, ErrorResponse>(
            new Request
            {
                Id = 1890,
            });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _appFixture.PublisherClient.GETAsync<Endpoint, Request, Response>(
            new Request
            {
                Id = 1890,
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.ResourceId.Should().Be(1699);
        result.DisplayName.Should().Be("Architecture");
    }
}