using System.Text.RegularExpressions;
using Aquifer.Common.Utilities;

namespace Aquifer.Jobs.Services.TranslationProcessors;

public sealed class FrenchTranslationProcessingService : ILanguageSpecificTranslationProcessingService
{
    private const string NarrowNonBreakingSpace = "\u202f";
    private const string NonBreakingSpace = "\u00a0";
    private const string HtmlEncodedNonBreakingSpace = "&nbsp;";

    private static readonly IReadOnlySet<string> s_regexItemsThatHaveNnbspAfterSelf = new HashSet<string>
    {
        @"\bv\.", // v.
        @"\bp\.", // p.
        "«",
        "=",
        @"\bapr\.", // apr.
        @"\bav\." // av.
    };

    private static readonly IReadOnlySet<string> s_regexItemsThatHaveNnbspBeforeSelf = new HashSet<string>
    {
        "»",
        "!",
        @"\?",
        ":",
        ";"
    };

    private readonly string _nnbspAfterPattern = @$"({string.Join("|", s_regexItemsThatHaveNnbspAfterSelf)})(?=[A-Za-zà-üÀ-Ü0-9\x20])";
    private readonly string _nnbspBeforePattern = @$"(\b|\x20)({string.Join("|", s_regexItemsThatHaveNnbspBeforeSelf)})";

    public string Iso6393Code => "FRA";

    public async Task<string> PostProcessHtmlAsync(string html, CancellationToken cancellationToken)
    {
        var processedHtml = await HtmlUtilities.ProcessHtmlTextContentAsync(
            html,
            async text =>
            {
                text = await SetNarrowNonBreakingSpacesAsync(text);
                text = await SetCorrectBibleBookAbbreviationAsync(text);

                return text;
            });

        return processedHtml;
    }

    private Task<string> SetNarrowNonBreakingSpacesAsync(string text)
    {
        text = text.Replace(NonBreakingSpace, NarrowNonBreakingSpace).Replace(HtmlEncodedNonBreakingSpace, NarrowNonBreakingSpace);

        text = Regex.Replace(text, _nnbspAfterPattern, m => m.Value + NarrowNonBreakingSpace);
        text = Regex.Replace(text, _nnbspBeforePattern, m => NarrowNonBreakingSpace + m.Value);

        // The regex matches the value but not the character before/after, so if a space comes before/after we'll end up with two spaces.
        text = text.Replace($"{NarrowNonBreakingSpace} ", NarrowNonBreakingSpace)
            .Replace($" {NarrowNonBreakingSpace}", NarrowNonBreakingSpace)
            // Replace this dash specifically with non-breaking dash
            .Replace("J.-C.", "J.‑C.");

        return Task.FromResult(text);
    }

    private static Task<string> SetCorrectBibleBookAbbreviationAsync(string text)
    {
        text = CommonRegex.BibleBookPrefixInVerse()
            .Replace(
                text,
                m =>
                {
                    var correctedValue = m.Value.Trim().Replace(".", "");
                    correctedValue = correctedValue.ToLower().Replace(NarrowNonBreakingSpace, " ") switch
                    {
                        "gen" or "ge" => "Gn",
                        "exod" => "Ex",
                        "lev" or "lév" => "Lv",
                        "nomb" => "Nb",
                        "deut" => "Dt",
                        "josh" => "Jos",
                        "ju" or "juges" => "Jg",
                        "ruth" => "Rt",
                        "1 s" or "1 sa" or "1 sam" => "1S",
                        "2 s" or "2 sa" or "2 sam" => "2S",
                        "1 r" or "1 rois" => "1R",
                        "2 r" or "2 rois" => "2R",
                        "1 ch" or "1 chr" => "1Ch",
                        "2 ch" or "2 chr" => "2Ch",
                        "ezra" or "esdras" => "Esd",
                        "neh" or "néh" => "Né",
                        "job" => "Jb",
                        "psa" or "pss" => "Ps",
                        "prov" or "pro" or "prv" => "Pr",
                        "ecc" => "Ec",
                        "sg" => "Ct",
                        "is" or "isa" or "és" => "Es",
                        "jer" or "jér" => "Jr",
                        "lam" => "Lm",
                        "éz" => "Ez",
                        "da" or "dan" => "Dn",
                        "hos" => "Os",
                        "joel" or "joël" or "joe" => "Jl",
                        "amos" => "Am",
                        "ob" => "Ab",
                        "jonah" or "jonas" => "Jon",
                        "mic" => "Mi",
                        "nah" => "Na",
                        "hab" => "Ha",
                        "zeph" or "soph" => "So",
                        "hag" => "Ag",
                        "zech" or "zach" or "zec" or "zac" => "Za",
                        "mal" => "Ml",
                        "matt" => "Mt",
                        "mk" or "marc" => "Mc",
                        "tob" => "Tb",
                        "1 macc" => $"1{NarrowNonBreakingSpace}M",
                        "2 macc" => $"2{NarrowNonBreakingSpace}M",
                        "sa" => "Sg",
                        "sir" => "Si",
                        "bar" => "Ba",
                        "lk" or "luc" => "Lc",
                        "jean" or "john" => "Jn",
                        "acts" or "actes" => "Ac",
                        "ro" or "rom" => "Rm",
                        "1 c" or "1 co" or "1 cor" => "1Co",
                        "2 c" or "2 co" or "2 cor" => "2Co",
                        "gal" => "Ga",
                        "éph" or "eph" or "ép" => "Ep",
                        "phil" => "Ph",
                        "1 th" or "1 the" or "1 thess" => "1Th",
                        "2 th" or "2 the" or "2 thess" => "2Th",
                        "1 ti" or "1 tm" or "1 tim" => "1Tm",
                        "2 ti" or "2 tm" or "2 tim" => "2Tm",
                        "tite" => "Ti",
                        "heb" or "héb" => "Hé",
                        "james" or "ja" or "jac" => "Jc",
                        "1 p" or "1 pi" or "1 pe" => "1P",
                        "2 p" or "2 pi" or "2 pe" => "2P",
                        "1 j" or "1 jn" => "1Jn",
                        "2 j" or "2 jn" => "2Jn",
                        "3 j" or "3 jn" => "3Jn",
                        "jud" or "ju" => "Jd",
                        "re" or "rév" or "rev" => "Ap",
                        _ => correctedValue
                    };

                    return correctedValue + " ";
                });

        return Task.FromResult(text);
    }
}