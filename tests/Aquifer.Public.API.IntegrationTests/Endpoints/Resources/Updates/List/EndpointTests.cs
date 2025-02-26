using System.Net;
using Aquifer.Public.API.Endpoints.Resources.Updates.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Updates.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            LanguageId = 4,
            ResourceCollectionCode = "TyndaleBibleDictionary",
            Timestamp = DateTime.UtcNow.AddMonths(-1),
            Limit = 2,
        },
        new Request
        {
            LanguageCode = "spa",
            Offset = 10,
            Limit = 15,
            Timestamp = DateTime.UtcNow.AddDays(-90).AddHours(1),
        },
        new Request
        {
            Timestamp = DateTime.UtcNow.AddDays(-90).AddHours(1),
            Limit = 100,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        // assert that response items are populated and match the request
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCountLessThanOrEqualTo(request.Limit);
        result.Offset.Should().Be(request.Offset);
        result.ReturnedItemCount.Should().Be(result.Items.Count);
        result.TotalItemCount.Should().BeGreaterThanOrEqualTo(request.Offset + result.ReturnedItemCount);

        foreach (var item in result.Items)
        {
            if (request.LanguageCode is not null)
            {
                item.LanguageCode.Should().Be(request.LanguageCode);
            }
            else
            {
                item.LanguageCode.Should().NotBeNullOrEmpty();
            }

            if (request.LanguageId is not null)
            {
                item.LanguageId.Should().Be(request.LanguageId);
            }
            else
            {
                item.LanguageId.Should().BeGreaterThan(0);
            }

            item.ResourceId.Should().BeGreaterThan(0);
            item.Timestamp.Should().BeAfter(request.Timestamp);
        }
    }
}