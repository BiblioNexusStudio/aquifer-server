using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aquifer.Common.Messages;

public sealed class MessagesJsonSerializer
{
    private static readonly JsonSerializerSettings s_settings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(),
        },
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Include,
    };

    public static T? Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, s_settings);
    }

    public static string Serialize<T>(T dto)
    {
        return JsonConvert.SerializeObject(dto, s_settings);
    }
}
