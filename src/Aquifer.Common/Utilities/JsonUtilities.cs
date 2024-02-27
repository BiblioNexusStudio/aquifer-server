using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aquifer.Common.Utilities;

public static class JsonUtilities
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
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
}