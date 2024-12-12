using System.Net;
using Aquifer.Public.API.Endpoints.Bibles.List;
using FastEndpoints.Testing;
using FastEndpoints;

namespace Aquifer.Public.API.IntegrationTests;

public sealed class BiblesListTests(AppFixture _appFixture) : TestBase<AppFixture>
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

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(result, r => r.Abbreviation == "BSB");
    }
}