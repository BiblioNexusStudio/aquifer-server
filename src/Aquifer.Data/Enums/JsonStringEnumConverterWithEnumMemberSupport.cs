using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aquifer.Data.Enums;

public sealed class JsonStringEnumConverterWithEnumMemberSupport<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum>
: JsonStringEnumConverter<TEnum> where TEnum : struct, Enum
{
    public JsonStringEnumConverterWithEnumMemberSupport() : base(namingPolicy: ResolveNamingPolicy())
    {
    }

    private static JsonNamingPolicy? ResolveNamingPolicy()
    {
        var map = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (f.Name, AttributeName: f.GetCustomAttribute<EnumMemberAttribute>()?.Value))
            .Where(pair => pair.AttributeName != null)
            .ToDictionary();

        return map.Count > 0 ? new EnumMemberNamingPolicy(map!) : null;
    }

    private sealed class EnumMemberNamingPolicy(Dictionary<string, string> map) : JsonNamingPolicy
    {
        public override string ConvertName(string name)
            => map.TryGetValue(name, out string? newName) ? newName : name;
    }
}

