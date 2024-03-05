using Aquifer.Common.Tiptap;
using ReverseMarkdown;

namespace Aquifer.Common.Utilities;

public static class TiptapUtilities
{
    // Note that this method isn't concerned about the return type since it's assumed it will be json serialized as part of an
    // http response. If more control over the return type is needed, create a new method.
    public static object ConvertFromJson(string tiptapJson, TiptapContentType type)
    {
        return type switch
        {
            TiptapContentType.None => JsonUtilities.DefaultDeserialize(tiptapJson),
            TiptapContentType.Json => Deserialize(tiptapJson).Select(x => x.Tiptap.Content),
            TiptapContentType.Html => GetHtmlFromJson(tiptapJson),
            TiptapContentType.Markdown => GetMarkdownFromJson(tiptapJson),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static List<TiptapModel> Deserialize(string json)
    {
        return JsonUtilities.DefaultDeserialize<List<TiptapModel>>(json);
    }

    private static IEnumerable<string> GetMarkdownFromJson(string tiptapJson)
    {
        var html = GetHtmlFromJson(tiptapJson);
        Converter converter = new();

        return html.Select(x => converter.Convert(x));
    }

    private static IEnumerable<string> GetHtmlFromJson(string tiptapJson)
    {
        return TiptapToHtmlConverter.ConvertFromJson(Deserialize(tiptapJson));
    }
}