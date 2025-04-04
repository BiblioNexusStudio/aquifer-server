﻿using System.Net;
using Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.List;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.API.IntegrationTests.Endpoints.Marketing.ParentResourceStatuses.List;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    [Fact]
    public async Task NoAuthenticationAndNoApiKey_ShouldReturnSuccess()
    {
        var request = new Request
        {
            LanguageId = Aquifer.Common.Constants.EnglishLanguageId,
        };

        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, IReadOnlyList<Response>>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(r => r.Title == "Berean Standard Bible");
    }
}