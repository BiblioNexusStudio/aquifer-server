using HtmlAgilityPack;

namespace Aquifer.Common.Utilities;

public static class HtmlUtilities
{
    public static IEnumerable<(string name, string html)> GetChunks(string html)
    {
        HtmlDocument doc = new();
        doc.LoadHtml(html);

        return doc.DocumentNode.ChildNodes.Select(x => (x.Name, x.OuterHtml));
    }
}