using System.Net;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Endpoints.Resources.Get;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Aquifer.Public.API.IntegrationTests.Endpoints.Resources.Get;

public sealed class EndpointTests(App _app) : TestBase<App>
{
    private const int ImageResourceContentId = 1372;
    private const int TextResourceContentId = 1438;

    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            ContentId = ImageResourceContentId,
            ContentTextType = TiptapContentType.Json,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Json,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Html,
        },
        new Request
        {
            ContentId = TextResourceContentId,
            ContentTextType = TiptapContentType.Markdown,
        },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public async Task ValidRequest_ShouldReturnSuccess(Request request)
    {
        var (response, result) = await _app.Client.GETAsync<Endpoint, Request, Response>(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();

        ValidateResponse(result, request);
    }

    internal static void ValidateResponse(Response result, Request request, string? expectedLanguageCode = null)
    {
        // assert that returned data matches the request
        result.Id.Should().Be(request.ContentId);

        if (expectedLanguageCode is not null)
        {
            result.Language.Code.Should().Be(expectedLanguageCode);
        }

        result.Content.Should().NotBeNull();
        var json = JsonUtilities.DefaultSerialize(result.Content);

        if (result.Grouping.MediaType == ResourceContentMediaType.Text.ToString())
        {
            if (request.ContentTextType is TiptapContentType.Json)
            {
                var tiptapModels = JsonUtilities.DefaultDeserialize<List<TiptapModel<TiptapNodeFiltered>>>(json);
                tiptapModels.Should().NotBeEmpty();
                tiptapModels.First().Tiptap.Content.Should().NotBeNullOrEmpty();
            }
            else if (request.ContentTextType is TiptapContentType.Html)
            {
                var htmlItems = JsonUtilities.DefaultDeserialize<string[]>(json);
                htmlItems.First().Should().Contain("<h1>");
            }
            else if (request.ContentTextType is TiptapContentType.Markdown)
            {
                var markdownItems = JsonUtilities.DefaultDeserialize<string[]>(json);
                markdownItems.First().Should().Contain("# ");
            }
            else
            {
                throw new InvalidOperationException($"Unexpected {nameof(request.ContentTextType)}: \"{request.ContentTextType}\".");
            }
        }
        else if (result.Grouping.MediaType == ResourceContentMediaType.Audio.ToString() ||
            result.Grouping.MediaType == ResourceContentMediaType.Video.ToString() ||
            result.Grouping.MediaType == ResourceContentMediaType.Image.ToString())
        {
            var cdnUrlWrapper = JsonUtilities.DefaultDeserialize<CdnUrlWrapper>(json);
            cdnUrlWrapper.Url.Should().NotBeNullOrEmpty();
        }
        else
        {
            throw new InvalidOperationException($"Unexpected {nameof(result.Grouping.MediaType)}: \"{result.Grouping.MediaType}\".");
        }

        // assert that all response data is populated
        result.LocalizedName.Should().NotBeNullOrEmpty();
        result.Name.Should().NotBeNullOrEmpty();
        result.ReferenceId.Should().BeGreaterThan(0);

        result.Grouping.Name.Should().NotBeNullOrEmpty();
        result.Grouping.Type.Should().NotBe(ResourceType.None);
        result.Grouping.MediaType.Should().NotBeNullOrEmpty();

        result.Grouping.LicenseInfo.Should().NotBeNull();
        result.Grouping.LicenseInfo.Copyright.Holder.Name.Should().NotBeNullOrEmpty();
        result.Grouping.LicenseInfo.Copyright.Holder.Url.Should().NotBeNullOrEmpty();
        result.Grouping.LicenseInfo.Title.Should().NotBeNullOrEmpty();
        foreach (var license in result.Grouping.LicenseInfo.Licenses)
        {
            license.Eng.Name.Should().NotBeNullOrEmpty();
            license.Eng.Url.Should().NotBeNullOrEmpty();
        }

        result.Language.DisplayName.Should().NotBeNullOrEmpty();
        result.Language.Code.Should().NotBeNullOrEmpty();
        result.Language.Id.Should().BeGreaterThan(0);
        result.Language.ScriptDirection.Should().NotBe(ScriptDirection.None);
    }

    private sealed class CdnUrlWrapper
    {
        public string Url { get; set; } = null!;
    }
}