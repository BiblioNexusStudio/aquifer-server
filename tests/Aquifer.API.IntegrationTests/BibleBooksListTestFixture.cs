using FastEndpoints;
using System.Net;
using Aquifer.API.Endpoints.BibleBooks.List;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests;

public sealed class BibleBooksListTestFixture(AppFixture _appFixture) : TestBase<AppFixture>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, result) = await _appFixture.Client.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().HaveCountGreaterThan(80);
        result.Should().Contain(r => r.Code == "4MA");
    }
}