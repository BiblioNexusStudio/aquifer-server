using System.Net;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Endpoints.Resources.Collections.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Collections.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            ResourceType = ResourceType.Dictionary,
        },
        new Request
        {
            Offset = 10,
            Limit = 10,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized(Request request)
    {
        var (response, results) = await _app.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        results.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();

        // assert that response data matches the request
        results.Count.Should().BeLessThanOrEqualTo(request.Limit);
        if (request.ResourceType != ResourceType.None)
        {
            results.Should().OnlyContain(r => r.ResourceType == request.ResourceType);
        }

        // assert that all response data is populated
        foreach (var result in results)
        {
            result.ResourceType.Should().NotBe(ResourceType.None);
            result.Code.Should().NotBeNullOrEmpty();
            result.DisplayName.Should().NotBeNullOrEmpty();
            result.ShortName.Should().NotBeNullOrEmpty();
        }
    }
}