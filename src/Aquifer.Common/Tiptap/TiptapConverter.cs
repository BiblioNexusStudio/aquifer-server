using System.Text.Json.Serialization;
using Aquifer.Common.Utilities;
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
    /// </summary>
    public static IReadOnlyList<string> ConvertJsonToHtmlItems(string json)
    {
        var tiptapModels = PartialDeserialize(json);

        using TiptapUtilities tiptapUtilities = new();

        // if there aren't multiple steps then just translate the argument JSON
        if (tiptapModels.Count == 1)
        {
            return [tiptapUtilities.ParseJsonAsHtml(json)];
        }

        // If there's more than one step then each step needs to be wrapped in its own Tiptap model array
        // (StepNumber doesn't matter) in order for TiptapUtilities to parse the JSON correctly.
        return tiptapModels
            .OrderBy(tm => tm.StepNumber)
            .Select(tm => tiptapUtilities.ParseJsonAsHtml(
                PartialSerialize(
                    [
                        new PartialTiptapModel
                        {
                            Tiptap = tm.Tiptap,
                            StepNumber = null,
                        }
                    ])))
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

    private static List<PartialTiptapModel> PartialDeserialize(string json)
    {
        return JsonUtilities.DefaultDeserialize<List<PartialTiptapModel>>(json);
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
        public required string Tiptap { get; init; }
    }
}