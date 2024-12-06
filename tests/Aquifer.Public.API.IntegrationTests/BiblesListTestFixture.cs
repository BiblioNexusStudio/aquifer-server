using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.List;
using FastEndpoints.Testing;
using FastEndpoints;

namespace Aquifer.Public.API.IntegrationTests;

public sealed class BiblesListTestFixture(AppFixture _appFixture) : TestBase<AppFixture>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _appFixture.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(
            new Request
            {
                IsLanguageDefault = true,
                LanguageId = 1,
                HasAudio = true,
                HasGreekAlignment = true,
            });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Should().Contain(r => r.Abbreviation == "BSB");
    }
}