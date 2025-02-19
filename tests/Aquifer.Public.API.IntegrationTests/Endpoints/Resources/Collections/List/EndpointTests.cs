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
        new()
        {
            ResourceType = ResourceType.Dictionary,
        },
        new()
        {
            Offset = 10,
            Limit = 10,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        // assert that response data matches the request
        result.Count.Should().BeLessThan(request.Limit);
        if (request.ResourceType != ResourceType.None)
        {
            result.Should().OnlyContain(r => r.ResourceType == request.ResourceType);
        }

        // assert that all response data is populated
        foreach (var collection in result)
        {
            collection.ResourceType.Should().NotBe(ResourceType.None);
            collection.Code.Should().NotBeNullOrEmpty();
            collection.DisplayName.Should().NotBeNullOrEmpty();
            collection.ShortName.Should().NotBeNullOrEmpty();
        }
    }
}