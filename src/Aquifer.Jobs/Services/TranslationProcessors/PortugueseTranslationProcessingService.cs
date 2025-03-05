using Aquifer.Common.Utilities;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public sealed class PortugueseTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    private const string NarrowNonBreakingSpace = "\u202f";
    private const string NonBreakingSpace = "\u00a0";
    private const string HtmlEncodedNonBreakingSpace = "&nbsp;";

    public string Iso6393Code => "POR";

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
            .Replace("por exemplo", "e.g.")
            .Replace("Por exemplo", "E.g.");

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
                        "gen" or "gên" or "ge" => "Gn",
                        "ex" or "exod" => "Êx",
                        "lev" => "Lv",
                        "num" => "Nm",
                        "deut" or "deu" => "Dt",
                        "josh" => "Js",
                        "ju" or "jud" => "Jz",
                        "ruth" => "Rt",
                        "1 sam" or "1 sm" or "1sam" => "1Sm",
                        "2 sam" or "2 sm" or "2sam" => "2Sm",
                        "1 rn" => "1Rn",
                        "2 rn" => "2Rn",
                        "1 reis" or "1 rs" => "1Rs",
                        "2 reis" or "2 rs" => "2Rs",
                        "3 rn" or "3 re" => "3Rn",
                        "4 rn" or "4 re" => "4Rn",
                        "1 cron" or "1 cr" => "1Cr",
                        "2 cron" or "2 cr" => "2Cr",
                        "esd" => "Ed",
                        "nee" or "neh" => "Ne",
                        "est" => "Et",
                        "jo" => "Jó",
                        "sal" => "Sl",
                        "prov" => "Pv",
                        "ecc" => "Ec",
                        "song" => "Ct",
                        "isa" => "Is",
                        "jer" or "je" => "Jr",
                        "lam" => "Lm",
                        "eze" or "ezeq" => "Ez",
                        "dan" => "Dn",
                        "hos" => "Os",
                        "joel" => "Jl",
                        "amos" => "Am",
                        "obad" => "Ob",
                        "jon" or "jo" => "Jn",
                        "mi" => "Mq",
                        "nah" => "Na",
                        "hab" => "Hc",
                        "sof" => "Sf",
                        "age" => "Ag",
                        "zac" => "Zc",
                        "mal" => "Ml",
                        "tob" => "Tb",
                        "jdt" => "Jt",
                        "add esth" => "Ad Est",
                        "wis" => "Sb",
                        "sir" => "Si",
                        "bar" => "Br",
                        "1 macc" => "1Mc",
                        "2 macc" => "2Mc",
                        "1 esd" => "1Ed",
                        "3 macc" => "3Mc",
                        "2 esd" => "2Ed",
                        "4 macc" => "4Mc",
                        "mat" => "Mt",
                        "marcos" or "mar" => "Mc",
                        "lu" or "luc" => "Lc",
                        "john" => "Jo",
                        "atos" => "At",
                        "rom" => "Rm",
                        "1 cor" or "1 co" => "1Co",
                        "2 cor" or "2 co" => "2Co",
                        "gal" => "Gl",
                        "eph" => "Ef",
                        "fil" or "fl" => "Fp",
                        "col" => "Cl",
                        "1 ts" or "1 tes" => "1Ts",
                        "2 ts" or "2 tes" => "2Ts",
                        "1 tm" or "1 tim" => "1Tm",
                        "2 tm" or "2 tim" => "2Tm",
                        "ti" => "Tt",
                        "heb" => "Hb",
                        "ti" or "tia" => "Tg",
                        "1 pe" => "1Pe",
                        "2 pe" => "2Pe",
                        "1 jo" => "1Jo",
                        "2 jo" => "2Jo",
                        "3 jo" => "3Jo",
                        "apo" => "Ap",
                        _ => correctedValue
                    };

                    return correctedValue + " ";
                });

        return Task.FromResult(text);
    }
}