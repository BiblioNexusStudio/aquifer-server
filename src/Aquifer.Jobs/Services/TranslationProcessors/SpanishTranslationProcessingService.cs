using Aquifer.Common.Utilities;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public class SpanishTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    private const string NarrowNonBreakingSpace = "\u202f";
    private const string NonBreakingSpace = "\u00a0";
    private const string HtmlEncodedNonBreakingSpace = "&nbsp;";

    public string Iso6393Code => "SPA";

    public async Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        var processedHtml = await HtmlUtilities.ProcessHtmlTextContentAsync(
            html,
            async text =>
            {
                // This order matters because the book abbreviation can remove trailing periods
                text = await SetCorrectBibleBookAbbreviationAsync(text);
                text = await ReplaceCommonMistakesAsync(text);

                return text;
            });

        return processedHtml;
    }

    private static Task<string> ReplaceCommonMistakesAsync(string text)
    {
        text = text.Replace(" cap ", " cap. ")
            .Replace(" Cap ", " Cap. ")
            .Replace(" caps ", " caps. ")
            .Replace(" Caps ", " Caps. ")
            .Replace("e.g.", "por ejemplo");

        return Task.FromResult(text);
    }

    private static Task<string> SetCorrectBibleBookAbbreviationAsync(string text)
    {
        text = text.Replace(HtmlEncodedNonBreakingSpace, " ");
        text = CommonRegex.BibleBookPrefixInVerse()
            .Replace(
                text,
                m =>
                {
                    var correctedValue = m.Value
                        .Trim()
                        .Replace(".", "")
                        .Replace(NarrowNonBreakingSpace, " ")
                        .Replace(NonBreakingSpace, " ");

                    correctedValue = correctedValue.ToLower() switch
                    {
                        "ge" or "gén" or "gen" => "Gn",
                        "éx" => "Ex",
                        "lev" => "Lv",
                        "núm" or "num" => "Nm",
                        "deut" or "deu" => "Dt",
                        "jue" => "Jc",
                        "ruth" => "Rt",
                        "1s" or "1sam" or "1sm" => "1 Sam",
                        "2s" or "2sam" or "2sm" => "2 Sam",
                        "1re" or "1kgs" or "1 kgs" or "1reyes" => "1 Re",
                        "2re" or "2kgs" or "1 kgs" or "2reyes" => "2 Re",
                        "1 cró" or "1cro" or "1chr" or "1cr" => "1 Cro",
                        "2 cró" or "2cro" or "2chr" or "2cr" => "2 Cro",
                        "es" or "ez" or "esd" => "Esd",
                        "neh" => "Ne",
                        "et" or "esth" => "Est",
                        "jo" => "Jb",
                        "s" or "salm" or "psalm" => "Sal",
                        "pv" or "prov" or "pro" => "Pr",
                        "eclo" => "Ec",
                        "qoh" => "Qo",
                        "ct" => "Cant",
                        "isa" => "Is",
                        "jer" => "Jr",
                        "dan" => "Dn",
                        "miq" or "mq" => "Mi",
                        "na" => "Nah",
                        "hc" => "Ha",
                        "hg" or "hageo" => "Ag",
                        "zc" => "Za",
                        "mal" => "Ml",
                        "to" => "Tb",
                        "jud" or "jt" => "Jdt",
                        "matt" => "Mt",
                        "mark" or "mar" => "Mc",
                        "acts" => "Hc",
                        "ro" or "rom" => "Ro",
                        "1co" or "1cor" => "1 Co",
                        "2co" or "2cor" => "2 Co",
                        "gl" or "gal" or "gál" => "Ga",
                        "flp" or "fl" => "Fil",
                        "cl" => "Col",
                        "1ts" or "1tes" => "1 Ts",
                        "2ts" or "2tes" => "2 Ts",
                        "1tim" or "1 tim" => "1 Tm",
                        "2tim" or "2 tim" => "2 Tm",
                        "ti" => "Tt",
                        "fil" => "Flm",
                        "heb" => "Hb",
                        "stg" => "St",
                        "1pe" => "1 Pe",
                        "2pe" => "2 Pe",
                        "1jn" => "1 Jn",
                        "2jn" => "2 Jn",
                        "3jn" => "3 Jn",
                        "apoc" => "Ap",
                        _ => correctedValue
                    };

                    return correctedValue + " ";
                });

        return Task.FromResult(text);
    }
}