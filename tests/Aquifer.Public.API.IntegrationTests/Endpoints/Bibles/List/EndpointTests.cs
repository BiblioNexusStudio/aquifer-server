using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.List;
using FastEndpoints.Testing;
using FastEndpoints;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Bibles.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request, string> GetValidRequestData => new()
    {
        {
            new Request
            {
                IsLanguageDefault = true,
                LanguageId = 1,
                HasAudio = true,
                HasGreekAlignment = true,
            },
            "BSB"
        },
        {
            new Request
            {
                LanguageCode = "spa",
            },
            "RV1909"
        },
        {
            new Request(),
            "TBI"
        },
    };

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request, string expectedBibleAbbreviation)
    {
        var (response, results) = await _app.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        results.Should().NotBeNullOrEmpty();

        // assert response data is populated
        foreach (var result in results)
        {
            result.Abbreviation.Should().NotBeNullOrEmpty();
            result.Id.Should().BeGreaterThan(0);
            result.LanguageId.Should().BeGreaterThan(0);
            result.Name.Should().NotBeNullOrEmpty();

            result.LicenseInfo.Copyright.Holder.Name.Should().NotBeNullOrEmpty();
            result.LicenseInfo.Title.Should().NotBeNullOrEmpty();
            foreach (var license in result.LicenseInfo.Licenses)
            {
                license.Eng.Name.Should().NotBeNullOrEmpty();
                license.Eng.Url.Should().NotBeNullOrEmpty();
            }
        }

        // assert specific result is present
        results.Should().Contain(r => r.Abbreviation == expectedBibleAbbreviation);
    }
}