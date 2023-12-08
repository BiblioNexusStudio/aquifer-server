using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;

namespace Aquifer.API.Common;

public class CustomEnumRouteParam<TEnum> where TEnum : struct, Enum
{
    private static readonly ConcurrentDictionary<string, TEnum> EnumMemberCache =
        new ConcurrentDictionary<string, TEnum>();

    static CustomEnumRouteParam()
    {
        foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString())!;
            EnumMemberAttribute? attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) as EnumMemberAttribute;

            if (attribute != null && attribute.Value != null)
            {
                EnumMemberCache[attribute.Value] = enumValue;
            }
        }
    }

    public TEnum EnumValue;

    public static bool TryParse(string? value, IFormatProvider? provider, out CustomEnumRouteParam<TEnum> outEnum)
    {
        if (value != null && EnumMemberCache.TryGetValue(value, out TEnum enumValue))
        {
            outEnum = new CustomEnumRouteParam<TEnum> { EnumValue = enumValue };
            return true;
        }

        if (Enum.TryParse<TEnum>(value, true, out TEnum outParsedEnum))
        {
            outEnum = new CustomEnumRouteParam<TEnum> { EnumValue = outParsedEnum };
            return true;
        }

        outEnum = new CustomEnumRouteParam<TEnum> { EnumValue = default };
        return false;
    }
}
