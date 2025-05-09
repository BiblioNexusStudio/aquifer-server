using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Aquifer.Common.Utilities;

public static partial class HtmlUtilities
{
    private static readonly char[] s_separators = [' ', '\u00a0', '\t', '\r', '\n'];

    private static readonly IReadOnlySet<string> s_nodeNamesToSkip =
        new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
        {
            "script",
            "style",
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
    /// Returns a list of any HTML parsing errors.  If there are no errors then the list will be empty.
    /// Example error:
    /// <example>
    ///     <code>
    /// TagNotOpened (1:912): Start tag &lt;span),&gt; was not found. Source: &lt;span), but this time, israel could keep the spoils with god's permission (v&gt;&lt;/span),&gt;
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="html">The HTML to parse and validate.</param>
    /// <returns>Any HTML validation errors.</returns>
    public static IReadOnlyList<string> GetHtmlValidationErrors(string html)
    {
        HtmlDocument doc = new();
        doc.LoadHtml(html);
        return doc.ParseErrors
            .Select(e => $"{e.Code} ({e.Line}:{e.LinePosition}): {e.Reason}. Source: {e.SourceText}")
            .ToList();
    }

    /// <summary>
    /// Note: Returned plain text will not necessarily be formatted in a friendly human-readable way.
    /// </summary>
    public static string GetPlainText(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        return string.Join(
            ' ',
            doc.DocumentNode
                .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Text && !s_nodeNamesToSkip.Contains(n.ParentNode.Name))
                .Select(n => WebUtility.HtmlDecode(n.InnerText).Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t)));
    }

    /// <summary>
    /// Minifies HTML element attributes before processing and then reinstates the original element attributes after processing.
    /// Thus, processing should only act on the human-readable content, not directly on elements or attribute markdown.
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

    /// <summary>
    /// Extracts the text of each HTML node and runs the processor on the text, replacing the extracted text with the result.
    /// Thus, processing should only act on the human-readable content, not directly on elements or attribute markdown.
    /// Note that this doesn't HTML decode the text, as re-encoding might change more things that we expect and potentially cause
    /// issues. So if you need to act upon a potentially encoded character make sure you account for that.
    /// </summary>
    /// <param name="html">The original HTML.</param>
    /// <param name="processAsync">
    /// A function which takes in HTML and processes the readable text content without changing elements or attribute
    /// markdown.
    /// </param>
    /// <returns>The processed HTML.</returns>
    public static async Task<string> ProcessHtmlTextContentAsync(string html, Func<string, Task<string>> processAsync)
    {
        HtmlDocument doc = new();
        doc.LoadHtml(html);

        var textNodes = doc.DocumentNode
            .Descendants()
            .Where(n => n.NodeType == HtmlNodeType.Text && !s_nodeNamesToSkip.Contains(n.ParentNode.Name));

        foreach (var node in textNodes)
        {
            if (!string.IsNullOrWhiteSpace(node.InnerHtml))
            {
                node.InnerHtml = await processAsync(node.InnerHtml);
            }
        }

        return doc.DocumentNode.OuterHtml;
    }

    [GeneratedRegex("(?<=<span\\s)([^>]*)(?=>)")]
    private static partial Regex SpanAttributeRegex();
}