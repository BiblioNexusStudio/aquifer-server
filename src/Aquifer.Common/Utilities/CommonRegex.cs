using System.Text.RegularExpressions;

namespace Aquifer.Common.Utilities;

public static partial class CommonRegex
{
    /// <summary>
    /// Used to find Bible books next to verses. Currently, it gets used to replace known bad abbreviations with good abbreviations. For
    /// example, in something like
    /// <code>(Gn 31.27) réjouissances et festins (Ex. 32.17–18 ; És.5.12 ; 24.8–9) victoires gn militaires (2 Ch. 20.27–28)</code> it will
    /// find "Gn ", "Ex. ", "És.", and "2 Ch. " in capture group 1. It pulls spaces and punctuations after because this may not be consistent
    /// with the language and also need to be replaced.
    /// </summary>
    [GeneratedRegex(@"(?:\d+[^\S\r\n])?(\d?[a-zA-Zà-üÀ-Ü]+\.?[^\S\r\n]?)(?=[^\S\r\n]?\d+)")]
    public static partial Regex BibleBookPrefixInVerse();
}