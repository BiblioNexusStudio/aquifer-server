using System.Text.Json;

namespace Aquifer.API.Utilities;

public static class JsonUtilities
{
    public static T DefaultDeserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
    }

    public static object DefaultDeserialize(string json)
    {
        return DefaultDeserialize<object>(json);
    }
}