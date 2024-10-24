using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aquifer.Common.Utilities;

public static class JsonUtilities
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static readonly JsonSerializerOptions QueueMessageOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public static T DefaultDeserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions)!;
    }

    public static object DefaultDeserialize(string json)
    {
        return DefaultDeserialize<object>(json);
    }

    public static string DefaultSerialize(object value)
    {
        return JsonSerializer.Serialize(value, DefaultOptions);
    }

    /// <summary>
    /// Serializes the contents of a string value as raw JSON.  The string is validated as being an RFC 8259-compliant JSON payload.
    /// </summary>
    /// <example>
    /// [JsonConverter(typeof(JsonUtilities.RawJsonConverter))]
    /// public required string Data { get; init; } // JSON string
    /// </example>
    public sealed class RawJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            return doc.RootElement.GetRawText();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            // Setting skipInputValidation to true will result in better performance.
            // However, unless the data is guaranteed to be valid JSON it should not be used.
            writer.WriteRawValue(value, skipInputValidation: false);
        }
    }
}