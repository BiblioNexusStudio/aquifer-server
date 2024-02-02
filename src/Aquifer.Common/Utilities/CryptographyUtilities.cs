using System.Security.Cryptography;

namespace Aquifer.Common.Utilities;

public static class CryptographyUtilities
{
    private static readonly char[] LowerLetters = Enumerable.Range('A', 26).Select(Convert.ToChar).ToArray();
    private static readonly char[] UpperLetters = Enumerable.Range('a', 26).Select(Convert.ToChar).ToArray();
    private static readonly char[] Numbers = Enumerable.Range('0', 10).Select(Convert.ToChar).ToArray();

    private static readonly char[] SpecialCharacters =
    [
        '!',
        '@',
        '#',
        '$',
        '%',
        '^',
        '&',
        '*'
    ];

    public static string GenerateSimplePassword()
    {
        var lower = RandomNumberGenerator.GetString(LowerLetters, 4);
        var upper = RandomNumberGenerator.GetString(UpperLetters, 4);
        var numbers = RandomNumberGenerator.GetString(Numbers, 4);
        var special = RandomNumberGenerator.GetString(SpecialCharacters, 4);

        var all = (lower + upper + numbers + special).ToArray();
        Random.Shared.Shuffle(all);
        return new string(all);
    }
}