using System.Net;
using Aquifer.Common.Tiptap;
using Aquifer.Public.API.Endpoints.Resources.Get.ByLanguage;
using FastEndpoints;
using FastEndpoints.Testing;
using Response = Aquifer.Public.API.Endpoints.Resources.Get.Response;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Get.ByLanguage;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    private const string EnglishLangageCode = "eng";
    private const int ImageResourceContentId = 1372;
    private const int TextResourceContentId = 1438;

    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            ContentId = ImageResourceContentId,
            ContentTextType = TiptapContentType.Json,
            LanguageCode = EnglishLangageCode,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Json,
            LanguageCode = EnglishLangageCode,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Html,
            LanguageCode = EnglishLangageCode,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Markdown,
            LanguageCode = EnglishLangageCode,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task InvalidRequest_NoApiKey_ShouldReturnUnauthorized(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.AnonymousClient.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        Get.EndpointTests.ValidateResponse(result, request, request.LanguageCode);
    }
}