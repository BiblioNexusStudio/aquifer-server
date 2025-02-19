using System.Net;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Endpoints.Resources.Collections.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Collections.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    public static TheoryData<Request> GetValidRequestData =>
    [
        new()
        {
            Code = "TyndaleBibleDictionary",
            LanguageCodes = ["eng", "fra"],
        },
        new()
        {
            Code = "TyndaleStudyNotes",
            LanguageIds = [4, 11],
        },
        new()
        {
            Code = "TyndaleStudyNotesBookIntros",
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        // assert that response data matches the request
        result.Code.Should().Be(request.Code);

        if (request.LanguageCodes is not null)
        {
            result.AvailableLanguages.Select(l => l.LanguageCode).Order()
                .Should().Equal(request.LanguageCodes.Order());
        }
        else if (request.LanguageIds is not null)
        {
            result.AvailableLanguages.Select(l => l.LanguageId).Order()
                .Should().Equal(request.LanguageIds.Order());
        }

        // assert that response data is populated
        result.DisplayName.Should().NotBeNullOrEmpty();
        result.ResourceType.Should().NotBe(ResourceType.None);
        result.ShortName.Should().NotBeNullOrEmpty();

        result.AvailableLanguages.Should().NotBeEmpty();
        foreach (var availableLanguage in result.AvailableLanguages)
        {
            availableLanguage.DisplayName.Should().NotBeNullOrEmpty();
            availableLanguage.LanguageCode.Should().NotBeNullOrEmpty();
            availableLanguage.LanguageId.Should().BeGreaterThan(0);
            availableLanguage.ResourceItemCount.Should().BeGreaterThan(0);
        }

        result.LicenseInfo.Copyright.Holder.Name.Should().NotBeNullOrEmpty();
        result.LicenseInfo.Copyright.Holder.Url.Should().NotBeNullOrEmpty();
        result.LicenseInfo.Title.Should().NotBeNullOrEmpty();
        foreach (var license in result.LicenseInfo.Licenses)
        {
            license.Eng.Name.Should().NotBeNullOrEmpty();
            license.Eng.Url.Should().NotBeNullOrEmpty();
        }
    }
}