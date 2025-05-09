using System.Net;
using Aquifer.Public.API.Endpoints.Resources.Types.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Types.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task ValidRequest_ShouldReturnSuccess()
    {
        var (response, results) = await _app.AnonymousClient.GETAsync<Endpoint, IReadOnlyList<Response>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        results.Should().NotBeNullOrEmpty();

        // assert response data is populated
        foreach (var result in results)
        {
            result.Type.Should().NotBeNullOrEmpty();
            result.Collections.Should().NotBeNullOrEmpty();
            foreach (var collection in result.Collections)
            {
                collection.Code.Should().NotBeNullOrEmpty();
                collection.Title.Should().NotBeNullOrEmpty();

                collection.LicenseInformation.Copyright.Holder.Name.Should().NotBeNullOrEmpty();
                collection.LicenseInformation.Copyright.Holder.Url.Should().NotBeNullOrEmpty();
                collection.LicenseInformation.Title.Should().NotBeNullOrEmpty();
                foreach (var license in collection.LicenseInformation.Licenses)
                {
                    license.Eng.Name.Should().NotBeNullOrEmpty();
                    license.Eng.Url.Should().NotBeNullOrEmpty();
                }
            }
        }

        // assert specific results are present
        var bibleDictionaries = results.Should()
            .Contain(x => x.Type == "Bible Dictionaries")
            .Subject;
        bibleDictionaries.Collections.Should().Contain(x => x.Code == "TyndaleBibleDictionary");
    }
}