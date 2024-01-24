using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aquifer.Common.Utilities;

public static class JsonUtilities
{
    public static T DefaultDeserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
    }

    public static object DefaultDeserialize(string json)
    {
        return DefaultDeserialize<object>(json);
    }

    public static string DefaultSerialize(object value)
    {
        return JsonSerializer.Serialize(value,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
    }
}