using System.Text;
using Aquifer.Common.Extensions;

namespace Aquifer.Common.Tiptap;

internal class TiptapToHtmlConverter : BaseTiptapConverter
{
    internal static IEnumerable<string> ConvertFromJson(List<TiptapModel> tiptaps)
    {
        List<string> response = [];
        foreach (var tiptap in tiptaps)
        {
            var contents = tiptap.Tiptap.Content;
            StringBuilder sb = new();

            foreach (var content in contents)
            {
                AppendText(sb, content);
            }

            response.Add(sb.Replace("\"", "&quot;").ToString());
        }

        return response;
    }

    private static void AppendText(StringBuilder sb, TiptapRootContent content)
    {
        var innerContents = content.Content ?? [];

        if (content.Type == HeadingType)
        {
            var level = content.Attrs?.Level ?? 1;
            sb.Append("<h").Append(level).Append('>');

            foreach (var innerContent in innerContents)
            {
                if (innerContent.Type == TextContentType)
                {
                    sb.Append(innerContent.Text);
                }
            }

            sb.Append("</h").Append(level).Append('>');
        }

        if (content.Type == ParagraphType)
        {
            AppendParagraph(sb, innerContents);
        }

        if (content.Type == BulletListType)
        {
            AppendList(sb, innerContents, "ul");
        }

        if (content.Type == OrderedListType)
        {
            AppendList(sb, innerContents, "ol");
        }

        if (content.Type == ListItemType)
        {
            AppendList(sb, innerContents, "li");
        }
    }

    private static void AppendParagraph(StringBuilder sb, List<TiptapRootContent> contents)
    {
        sb.Append("<p>");

        foreach (var innerContent in contents)
        {
            if (innerContent.Type != TextContentType)
            {
                continue;
            }

            var text = innerContent.Text;
            var isBoldText = innerContent.Marks?.Any(x => x.Type == BoldMarkType) == true;
            var isItalicText = innerContent.Marks?.Any(x => x.Type == ItalicMarkType) == true;

            var bibleReference = innerContent.Marks?.Where(x => x.Type == BibleReferenceMarkType).FirstOrDefault();
            var resourceReference = innerContent.Marks?.Where(x => x.Type == ResourceReferenceMarkType).FirstOrDefault();
            var hasBibleReference = bibleReference is not null;
            var hasResourceReference = resourceReference is not null;

            if (hasBibleReference)
            {
                var verses = bibleReference!.Attrs?.Verses?.FirstOrDefault();
                var startVerse = verses?.StartVerse?.ToString();
                var endVerse = verses?.EndVerse?.ToString();

                var span = $"""
                            <span data-bnType="bibleReference" data-startVerse="{startVerse}" data-endVerse="{endVerse}">
                            """;

                sb.Append(span);
            }

            if (hasResourceReference)
            {
                var span = $"""
                            <span data-bnType="resourceReference" data-resourceType="{resourceReference!.Attrs?.ResourceType}" data-resourceId="{resourceReference.Attrs?.ResourceId}">
                            """;

                sb.Append(span);
            }

            sb.AppendIf("<em>", isItalicText).AppendIf("<strong>", isBoldText)
                .Append(text)
                .AppendIf("</strong>", isBoldText).AppendIf("</em>", isItalicText)
                .AppendIf("</span>", hasBibleReference).AppendIf("</span>", hasResourceReference);
        }

        sb.Append("</p>");
    }

    private static void AppendList(StringBuilder sb, List<TiptapRootContent> contents, string element)
    {
        sb.Append('<').Append(element).Append('>');

        foreach (var innerContent in contents)
        {
            AppendText(sb, innerContent);
        }

        sb.Append("</").Append(element).Append('>');
    }
}