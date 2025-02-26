using System.Net;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Data.Enums;
using Aquifer.Public.API.Endpoints.Resources.Search.GetResources;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Search.GetResources;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            Query = "Dios",
            LanguageCode = "spa",
        },
        new Request
        {
            LanguageCode = "arb",
            ResourceType = ResourceType.StudyNotes,
            Offset = 2,
            Limit = 10,
        },
        new Request
        {
            BookCode = BibleBookCodeUtilities.CodeFromId(BookId.BookMAT),
            StartChapter = 2,
            StartVerse = 3,
            EndChapter = 3,
            EndVerse = 2,
            LanguageId = 1,
            ResourceCollectionCode = "TyndaleBibleDictionary",
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
        result.Items.Should().NotBeNullOrEmpty();
        result.Items.Should().HaveCountLessThanOrEqualTo(request.Limit);
        result.Offset.Should().Be(request.Offset);
        result.ReturnedItemCount.Should().Be(result.Items.Count);
        result.TotalItemCount.Should().BeGreaterThanOrEqualTo(request.Offset + result.ReturnedItemCount);

        foreach (var item in result.Items)
        {
            item.Id.Should().BeGreaterThan(0);

            if (request.LanguageCode is not null)
            {
                item.LanguageCode.Should().Be(request.LanguageCode);
            }
            else
            {
                item.LanguageCode.Should().NotBeNullOrEmpty();
            }

            if (request.Query is not null)
            {
                item.LocalizedName.ToLower().Should().Contain(request.Query.ToLower());
            }
            else
            {
                item.LocalizedName.Should().NotBeNullOrEmpty();
            }

            item.MediaType.Should().NotBe(ResourceContentMediaType.None);
            item.Name.Should().NotBeNullOrEmpty();

            if (request.ResourceCollectionCode is not null)
            {
                item.Grouping.CollectionCode.Should().Be(request.ResourceCollectionCode);
            }
            else
            {
                item.Grouping.CollectionCode.Should().NotBeNullOrEmpty();
            }

            item.Grouping.CollectionTitle.Should().NotBeNullOrEmpty();
            item.Grouping.Name.Should().NotBeNullOrEmpty();

            if (request.ResourceType != ResourceType.None)
            {
                item.Grouping.Type.Should().Be(request.ResourceType);
            }
            else
            {
                item.Grouping.Type.Should().NotBe(ResourceType.None);
            }
        }
    }
}