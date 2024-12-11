using System.Text.RegularExpressions;
using Aquifer.Common.Utilities;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public sealed class FrenchTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    private const char NarrowNonBreakingSpace = '\u202f';
    private const char NonBreakingSpace = '\u00a0';
    private const string HtmlEncodedNonBreakingSpace = "&nbsp;";

    private static readonly IReadOnlySet<string> s_ItemsThatHaveNnbspAfterSelfAsRegex = new HashSet<string>
    {
        @"\bv\.", // v.
        @"\bp\.", // p.
        "«",
        "=",
        @"\bapr\.", // apr.
        @"\bav\." // av.
    };

    private static readonly IReadOnlySet<string> s_ItemsThatHaveNnbspBeforeSelfAsRegex = new HashSet<string>
    {
        "»",
        "!",
        @"\?",
        ":",
        ";"
    };

    private readonly string _nnbspAfterPattern = @$"({string.Join("|", s_ItemsThatHaveNnbspAfterSelfAsRegex)})(?=[A-Za-z0-9\x20])";
    private readonly string _nnbspBeforePattern = @$"(\b|\x20)({string.Join("|", s_ItemsThatHaveNnbspBeforeSelfAsRegex)})";

    public string Iso6393Code => "FRA";

    public async Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        var processedHtml = await HtmlUtilities.ProcessHtmlTextContentAsync(html, SetNarrowNonBreakingSpacesAsync);
        return processedHtml;
    }

    private Task<string> SetNarrowNonBreakingSpacesAsync(string text)
    {
        text = text.Replace(NonBreakingSpace, NarrowNonBreakingSpace);
        text = text.Replace(HtmlEncodedNonBreakingSpace, $"{NarrowNonBreakingSpace}");

        text = Regex.Replace(text, _nnbspAfterPattern, m => m.Value + NarrowNonBreakingSpace);
        text = Regex.Replace(text, _nnbspBeforePattern, m => NarrowNonBreakingSpace + m.Value);

        // The regex matches the value but not the character before/after, so if a space comes before/after we'll end up with two spaces.
        text = text.Replace($"{NarrowNonBreakingSpace} ", $"{NarrowNonBreakingSpace}")
            .Replace($" {NarrowNonBreakingSpace}", $"{NarrowNonBreakingSpace}")
            // Replace this dash specifically with non-breaking dash
            .Replace("J.-C.", "J.‑C.");

        return Task.FromResult(text);
    }
}