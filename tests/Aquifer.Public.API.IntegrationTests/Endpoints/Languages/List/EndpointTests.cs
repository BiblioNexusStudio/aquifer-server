using System.Net;
using Aquifer.Public.API.Endpoints.Languages.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Languages.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        result.Count.Should().BeGreaterThan(0);
        foreach (var language in result)
        {
            language.Id.Should().BeGreaterThan(0);
            language.Code.Should().NotBeNullOrEmpty();
            language.EnglishDisplay.Should().NotBeNullOrEmpty();
            language.LocalizedDisplay.Should().NotBeNull();
            language.ScriptDirection.Should().NotBeNullOrEmpty();
        }

        result.Should().Contain(x => x.Code == "fra" && x.EnglishDisplay == "French" && x.LocalizedDisplay == "Français");
    }
}