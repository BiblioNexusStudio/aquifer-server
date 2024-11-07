using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Aquifer.Common.Utilities;

public static partial class HtmlUtilities
{
    private static readonly char[] s_separators = [' ', '\u00a0', '\t', '\r', '\n'];
    private static readonly IReadOnlySet<string> s_nodeNamesToSkip = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
    {
        "script",
        "style"
    };

    public static IEnumerable<(string name, string html)> GetChunks(string html)
    {
        HtmlDocument doc = new();
        doc.LoadHtml(html);

        return doc.DocumentNode.ChildNodes.Select(x => (x.Name, x.OuterHtml));
    }

    public static int GetWordCount(string html)
    {
        return GetPlainText(html)
            .Split(s_separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Count(w => !w.All(char.IsPunctuation));
    }

    /// <summary>
    /// Note: Returned plain text will not necessarily be formatted in a friendly human-readable way.
    /// </summary>
    public static string GetPlainText(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        return string.Join(
            separator: ' ',
            doc.DocumentNode
                .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Text && !s_nodeNamesToSkip.Contains(n.ParentNode.Name))
                .Select(n => WebUtility.HtmlDecode(n.InnerText).Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t)));
    }

    /// <summary>
    /// Minifies HTML element attributes before processing and then reinstates the original element attributes after processing.
    /// Thus, processing should only act on the human-readable content, not directly elements or attribute markdown.
    /// </summary>
    /// <param name="html">The original HTML.</param>
    /// <param name="processAsync">A function which takes in HTML and processes the content without changing elements or attribute markdown.</param>
    /// <returns>The processed HTML.</returns>
    public static async Task<string> ProcessHtmlContentAsync(string html, Func<string, Task<string>> processAsync)
    {
        var (minifiedHtml, replacementByIndexMap) = MinifyHtml(html);
        var processedHtml = await processAsync(minifiedHtml);
        return ExpandHtml(processedHtml, replacementByIndexMap);

        static (string MinifiedHtml, IReadOnlyDictionary<int, string> ReplacementByIndexMap) MinifyHtml(string html)
        {
            var matches = SpanAttributeRegex().Matches(html);
            Dictionary<int, string> replacementByIndexMap = [];

            var minifiedHtml = new StringBuilder(html);

            // shorten by extracting spans (which don't need to be translated)
            for (var i = 0; i < matches.Count; i++)
            {
                replacementByIndexMap.Add(i, matches[i].Value);
                minifiedHtml.Replace(matches[i].Value, $"a=\"{i}\"");
            }

            return (minifiedHtml.ToString(), replacementByIndexMap);
        }

        static string ExpandHtml(string minifiedHtml, IReadOnlyDictionary<int, string> replacementByIndexMap)
        {
            if (replacementByIndexMap.Count == 0)
            {
                return minifiedHtml;
            }

            var expandedHtml = new StringBuilder(minifiedHtml);
            foreach (var replacement in replacementByIndexMap)
            {
                expandedHtml.Replace($"a=\"{replacement.Key}\"", replacement.Value);
            }

            return expandedHtml.ToString();
        }
    }

    [GeneratedRegex("(?<=<span\\s)([^>]*)(?=>)")]
    private static partial Regex SpanAttributeRegex();
}