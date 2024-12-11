using Aquifer.Jobs.Services.TranslationProcessors;

namespace Aquifer.Jobs.UnitTests.Services.TranslationProcessors;

public sealed class FrenchTranslationProcessingServiceTests
{
    private readonly FrenchTranslationProcessingService service = new();

    [Theory]
    [InlineData(
        /* lang=html */
        """<p>some v.when =3 5= 5 and p. fine nap. nav.okay m.t v,this <span>«this quote» or </span></p><ol><li>« this quote »</li></ol> and apr. J.-C. with av. J.-C.<p>Then: two ; get! space ? p.&nbsp;this</p>""",
        /* lang=html */
        """<p>some v. when = 3 5= 5 and p. fine nap. nav.okay m.t v,this <span>« this quote » or </span></p><ol><li>« this quote »</li></ol> and apr. J.‑C. with av. J.‑C.<p>Then : two ; get ! space ? p. this</p>""")]
    public async Task PostProcessHtmlAsync_WithMissingNonBreakingSpaces_ShouldReturnCorrectNonBreakingSpacesUsage(string html,
        string expectedHtml)
    {
        var response = await service.PostProcessHtmlAsync(html, default);
        Assert.Equal(expectedHtml, response);
    }
}