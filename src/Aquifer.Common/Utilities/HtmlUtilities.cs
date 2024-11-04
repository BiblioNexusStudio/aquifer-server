using System.Net;
using HtmlAgilityPack;

namespace Aquifer.Common.Utilities;

public static class HtmlUtilities
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
}