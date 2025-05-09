using Aquifer.Jobs.Services.TranslationProcessors;

namespace Aquifer.Jobs.UnitTests.Services.TranslationProcessors;

public class ChineseTranslationProcessingServiceTests
{
    private readonly SimplifiedChineseTranslationProcessingService _simplifiedService = new();
    private readonly TraditionalChineseTranslationProcessingService _traditionalService = new();

    [Theory]
    [InlineData(
        "在大衛王統治的末期,他的兒子們為繼承王位而爭鬥。主要的競爭者是亞多尼雅和所羅門。大祭司亞比亞他和大衛的將軍約押都支持亞多尼雅的王位繼承權(王上1:5–7).這可能是因為亞多尼雅是大衛最年長的在世繼承人.撒督支持所羅門,所羅門最終成為了王.由於亞比亞他沒有支持新王,他被流放到亞拿突(王上2:26–27),亞拿突是一個距耶路撒冷東北約四英...里(6:4公里)的小村莊.",
        "在大衛王統治的末期，他的兒子們為繼承王位而爭鬥。主要的競爭者是亞多尼雅和所羅門。大祭司亞比亞他和大衛的將軍約押都支持亞多尼雅的王位繼承權（王上1:5–7）。這可能是因為亞多尼雅是大衛最年長的在世繼承人。撒督支持所羅門，所羅門最終成為了王。由於亞比亞他沒有支持新王，他被流放到亞拿突（王上2:26–27），亞拿突是一個距耶路撒冷東北約四英......里（6:4公里）的小村莊。")]
    public async Task PostProcessHtmlAsync_WithHalfShapes_ShouldReturnFullShapes(string text, string expectedText)
    {
        var traditionalResponse = await _traditionalService.PostProcessHtmlAsync(text, TestContext.Current.CancellationToken);
        var simplifiedResponse = await _simplifiedService.PostProcessHtmlAsync(text, TestContext.Current.CancellationToken);

        Assert.Equal(expectedText, traditionalResponse);
        Assert.Equal(expectedText, simplifiedResponse);
    }
}