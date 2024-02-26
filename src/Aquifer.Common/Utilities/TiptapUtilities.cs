using Aquifer.Common.Models;
using Aquifer.Common.Tiptap;

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
            TiptapContentType.Markdown or TiptapContentType.Html =>
                TiptapToMarkdownConverter.ConvertFromJson(Deserialize(tiptapJson)),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static List<TiptapModel> Deserialize(string json)
    {
        return JsonUtilities.DefaultDeserialize<List<TiptapModel>>(json);
    }
}