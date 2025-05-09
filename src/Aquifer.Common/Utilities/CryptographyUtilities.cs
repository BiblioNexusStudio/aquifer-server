using System.Security.Cryptography;

namespace Aquifer.Common.Utilities;

public static class CryptographyUtilities
{
    private static readonly char[] s_lowerLetters = Enumerable.Range('A', 26).Select(Convert.ToChar).ToArray();
    private static readonly char[] s_upperLetters = Enumerable.Range('a', 26).Select(Convert.ToChar).ToArray();
    private static readonly char[] s_numbers = Enumerable.Range('0', 10).Select(Convert.ToChar).ToArray();

    private static readonly char[] s_specialCharacters =
    [
        '!',
        '@',
        '#',
        '$',
        '%',
        '^',
        '&',
        '*',
    ];

    public static string GenerateSimplePassword()
    {
        var lower = RandomNumberGenerator.GetString(s_lowerLetters, 4);
        var upper = RandomNumberGenerator.GetString(s_upperLetters, 4);
        var numbers = RandomNumberGenerator.GetString(s_numbers, 4);
        var special = RandomNumberGenerator.GetString(s_specialCharacters, 4);

        var all = (lower + upper + numbers + special).ToArray();
        Random.Shared.Shuffle(all);
        return new string(all);
    }
}