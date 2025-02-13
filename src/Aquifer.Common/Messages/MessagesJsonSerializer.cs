using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aquifer.Common.Messages;

public sealed class MessagesJsonSerializer
{
    // max Azure Storage Queue message size is 64 KB: https://learn.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction
    private const int MaxMessageSizeInBytes = 65_536;

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

    public static string Serialize<T>(T dto, bool shouldAllowInvalidMessageLength = false)
    {
        var json = JsonConvert.SerializeObject(dto, s_settings);

        if (!shouldAllowInvalidMessageLength && System.Text.Encoding.Unicode.GetByteCount(json) > MaxMessageSizeInBytes)
        {
            throw new ArgumentException(
                $"Serialized JSON message is {System.Text.Encoding.Unicode.GetByteCount(json)} bytes but must be less than {MaxMessageSizeInBytes} bytes. Content: {json}",
                nameof(dto));
        }

        return json;
    }
}
