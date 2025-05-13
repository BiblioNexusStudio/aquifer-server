using System.Net;
using Aquifer.Well.API.Endpoints.Languages.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Well.API.IntegrationTests.Endpoints.Languages.List;

public sealed class ListLanguagesEndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized()
    {
        var (response, results) = await _app.Client.GETAsync<ListLanguagesEndpoint, IReadOnlyList<ListLanguagesResponse>>();

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        results.Should().BeNull();
    }

    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<ListLanguagesEndpoint, IReadOnlyList<ListLanguagesResponse>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();

        results.Count.Should().BeGreaterThan(0);
        foreach (var result in results)
        {
            result.Id.Should().BeGreaterThan(0);
            result.Code.Should().NotBeNullOrEmpty();
            result.EnglishDisplay.Should().NotBeNullOrEmpty();
            result.LocalizedDisplay.Should().NotBeNull();
            result.ScriptDirection.Should().NotBeNullOrEmpty();
        }

        results.Should().Contain(x => x.Code == "fra" && x.EnglishDisplay == "French" && x.LocalizedDisplay == "Français");
    }
}