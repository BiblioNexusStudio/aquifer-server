using Aquifer.Common.Utilities;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public abstract class ChineseTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    public abstract string Iso6393Code { get; }

    public virtual async Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        var processedHtml = await HtmlUtilities.ProcessHtmlTextContentAsync(
            html,
            async text =>
            {
                text = await ReplaceHalfShapeCharactersAsync(text);

                return text;
            });

        return processedHtml;
    }

    private static Task<string> ReplaceHalfShapeCharactersAsync(string text)
    {
        // Colon ( : ) is intentionally not added here because it should not be full shape in verses.
        text = text.Replace('(', '（')
            .Replace(')', '）')
            .Replace(';', '；')
            .Replace('.', '。')
            .Replace(',', '，')
            .Replace('?', '？')
            .Replace("。。。", "......");

        return Task.FromResult(text);
    }
}