using Aquifer.Common.Models;

namespace Aquifer.Common.Tiptap;

internal abstract class BaseTiptapConverter
{
    protected const string HeadingType = "heading";
    protected const string ParagraphType = "paragraph";
    protected const string BulletListType = "bulletList";
    protected const string OrderedListType = "orderedList";
    protected const string ListItemType = "listItem";
    protected const string ItalicMarkType = "italic";
    protected const string BoldMarkType = "bold";
    protected const string TextContentType = "text";

    internal abstract List<string> ConvertFromJson(List<TiptapModel> tiptaps);
}