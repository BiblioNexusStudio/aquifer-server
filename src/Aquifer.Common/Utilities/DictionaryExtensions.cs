namespace Aquifer.Common.Utilities;

public static class DictionaryExtensions
{
    public static TValue? GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        where TValue : struct
    {
        return source.TryGetValue(key, out var value)
            ? value
            : null;
    }
}