using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
using ReverseMarkdown;
using TiptapUtilities = Aquifer.JsEngine.Tiptap.TiptapUtilities;

namespace Aquifer.Common.Tiptap;

/// <summary>
/// Uses the TiptapUtilities.js file (shared with front-end) to do conversion instead of the C# models in this project.
/// Unlike TiptapUtilities.js, this utility correctly handles step numbers for Content with more than one step.
/// </summary>
public static class TiptapConverter
{
    /// <summary>
    /// Converts a JSON string containing an array of Tiptap models to HTML strings, one for each Tiptap model.
    /// Note: this removes any comments that are present in the Tiptap.
    /// </summary>
    public static IReadOnlyList<string> ConvertJsonToHtmlItems(string json)
    {
        var tiptapModels = DeserializeForPublish(json);

        using TiptapUtilities tiptapUtilities = new();

        // Each step needs to be wrapped in its own Tiptap model array
        return tiptapModels
            .OrderBy(tm => tm.StepNumber)
            .Select(tm => tiptapUtilities.ParseJsonAsHtml(JsonUtilities.DefaultSerialize(new[] { tm })))
            .ToList();
    }

    /// <summary>
    /// Converts an array of HTML items, one for each step number and in step number order,
    /// into a JSON string containing an array of Tiptap models.
    /// </summary>
    public static string ConvertHtmlItemsToJson(IReadOnlyList<string> htmlItems)
    {
        var shouldInsertStepNumber = htmlItems.Count > 1;

        using TiptapUtilities tiptapUtilities = new();
        var partialTiptapModels = htmlItems
            .Select((html, index) => new PartialTiptapModel
            {
                Tiptap = tiptapUtilities.ParseHtmlAsJson(html),
                StepNumber = shouldInsertStepNumber ? index + 1 : null,
            })
            .ToList();

        return PartialSerialize(partialTiptapModels);
    }

    /// <summary>
    /// Converts a Tiptap JSON array string to an array of the specified type.
    /// Note: this method isn't concerned about the return type since it's assumed it will be json serialized as part of an
    /// http response. If more control over the return type is needed, create a new method.
    /// Note: this removes any comments that are present in the Tiptap.
    /// </summary>
    public static object ConvertJsonToType(string tiptapJson, TiptapContentType type)
    {
        return type switch
        {
            TiptapContentType.None => JsonUtilities.DefaultDeserialize(tiptapJson),
            TiptapContentType.Json => DeserializeForPublish(tiptapJson),
            TiptapContentType.Html => ConvertJsonToHtmlItems(tiptapJson),
            TiptapContentType.Markdown => ConvertJsonToMarkdownItems(tiptapJson),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    public static List<TiptapModel<TiptapNodeFiltered>> DeserializeForPublish(string json)
    {
        return JsonUtilities.DefaultDeserialize<List<TiptapModel<TiptapNodeFiltered>>>(json);
    }

    private static IEnumerable<string> ConvertJsonToMarkdownItems(string tiptapJson)
    {
        var html = ConvertJsonToHtmlItems(tiptapJson);
        Converter converter = new();

        return html.Select(x => converter.Convert(x));
    }

    private static string PartialSerialize(IEnumerable<PartialTiptapModel> partialTiptapModels)
    {
        return JsonUtilities.DefaultSerialize(partialTiptapModels);
    }

    /// <summary>
    /// This is purely a wrapper in order to serialize/deserialize the Tiptap model object array and step numbers.
    /// </summary>
    private class PartialTiptapModel
    {
        /// <summary>
        /// Will not be present in most ResourceContentVersion Content JSON.
        /// </summary>
        public int? StepNumber { get; init; }

        /// <summary>
        /// Keep the Tiptap JSON as-is.
        /// </summary>
        [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
        public required object Tiptap { get; init; }
    }
}