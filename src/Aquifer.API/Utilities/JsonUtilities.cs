using System.Text.Json;

namespace Aquifer.API.Utilities;

public static class JsonUtilities
{
    public static T DefaultSerialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default)!;
    }

    public static object DefaultSerialize(string json)
    {
        return DefaultSerialize<object>(json);
    }
}