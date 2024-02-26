using System.Text;
using Aquifer.Common.Extensions;
using Aquifer.Common.Models;

namespace Aquifer.Common.Tiptap;

internal class TiptapToMarkdownConverter : BaseTiptapConverter
{
    private const string MdNewLine = "<br>";

    internal override List<string> ConvertFromJson(List<TiptapModel> tiptaps)
    {
        List<string> response = [];
        foreach (var tiptap in tiptaps)
        {
            var contents = tiptap.Tiptap.Content;
            StringBuilder sb = new();

            foreach (var content in contents)
            {
                AppendText(sb, content, "root", 0);
            }

            response.Add(TrimEndingNewLines(sb.ToString()));
        }

        return response;
    }

    private static void AppendText(StringBuilder sb, TiptapRootContent content, string parent, int depth, int listItem = 0)
    {
        var innerContents = content.Content ?? [];

        if (content.Type == HeadingType)
        {
            int level = content.Attrs?.Level ?? 1;
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
            sb.Append(MdNewLine).AppendIf(MdNewLine, parent != ListItemType && content.Content?.Count > 0);
        }

        if (content.Type == BulletListType)
        {
            ++depth;
            foreach (var innerContent in innerContents)
            {
                AppendText(sb, innerContent, content.Type, depth);
            }

            sb.AppendIf(MdNewLine, parent != ListItemType);
        }

        if (content.Type == OrderedListType)
        {
            ++depth;
            foreach (var innerContent in innerContents)
            {
                AppendText(sb, innerContent, content.Type, depth, ++listItem);
            }

            sb.AppendIf(MdNewLine, parent != ListItemType);
        }

        if (content.Type == ListItemType)
        {
            for (var i = 0; i < depth; i++)
            {
                sb.Append(parent == BulletListType ? "- " : $"{listItem}. ");
            }

            foreach (var innerContent in innerContents)
            {
                AppendText(sb, innerContent, content.Type, depth);
            }
        }
    }

    private static void AppendParagraph(StringBuilder sb, List<TiptapRootContent> contents)
    {
        foreach (var innerContent in contents)
        {
            if (innerContent.Type != TextContentType)
            {
                continue;
            }

            var text = innerContent.Text;
            var trimmedStart = innerContent.Text.StartsWith(' ');
            if (trimmedStart)
            {
                text = text.TrimStart();
                trimmedStart = true;
            }

            var trimmedEnd = innerContent.Text.EndsWith(' ');
            if (trimmedEnd)
            {
                text = text.TrimEnd();
                trimmedEnd = true;
            }

            sb.AppendIf(' ', trimmedStart);
            AppendItalic(sb, innerContent);
            AppendBold(sb, innerContent);

            sb.Append(text);

            AppendItalic(sb, innerContent);
            AppendBold(sb, innerContent);
            sb.AppendIf(' ', trimmedEnd);
        }
    }

    private static void AppendBold(StringBuilder sb, TiptapRootContent innerContent)
    {
        sb.AppendIf("**", innerContent.Marks?.Any(x => x.Type == BoldMarkType) == true);
    }

    private static void AppendItalic(StringBuilder sb, TiptapRootContent innerContent)
    {
        sb.AppendIf('_', innerContent.Marks?.Any(x => x.Type == ItalicMarkType) == true);
    }

    private static string TrimEndingNewLines(string input)
    {
        while (input.Length > MdNewLine.Length && input[^MdNewLine.Length..input.Length] == MdNewLine)
        {
            input = input[..^MdNewLine.Length];
        }

        return input;
    }
}