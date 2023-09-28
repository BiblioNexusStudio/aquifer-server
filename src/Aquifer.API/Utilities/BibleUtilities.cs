using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aquifer.API.Utilities;

public static class BibleUtilities
{
    [JsonConverter(typeof(BookCodeConverter))]
    public enum BookCode
    {
        None = 0,
        GEN = 1,
        EXO = 2,
        LEV = 3,
        NUM = 4,
        DEU = 5,
        JOS = 6,
        JDG = 7,
        RUT = 8,
        _1SA = 9,
        _2SA = 10,
        _1KI = 11,
        _2KI = 12,
        _1CH = 13,
        _2CH = 14,
        EZR = 15,
        NEH = 16,
        EST = 17,
        JOB = 18,
        PSA = 19,
        PRO = 20,
        ECC = 21,
        SNG = 22,
        ISA = 23,
        JER = 24,
        LAM = 25,
        EZK = 26,
        DAN = 27,
        HOS = 28,
        JOL = 29,
        AMO = 30,
        OBA = 31,
        JON = 32,
        MIC = 33,
        NAM = 34,
        HAB = 35,
        ZEP = 36,
        HAG = 37,
        ZEC = 38,
        MAL = 39,
        MAT = 41,
        MRK = 42,
        LUK = 43,
        JHN = 44,
        ACT = 45,
        ROM = 46,
        _1CO = 47,
        _2CO = 48,
        GAL = 49,
        EPH = 50,
        PHP = 51,
        COL = 52,
        _1TH = 53,
        _2TH = 54,
        _1TI = 55,
        _2TI = 56,
        TIT = 57,
        PHM = 58,
        HEB = 59,
        JAS = 60,
        _1PE = 61,
        _2PE = 62,
        _1JN = 63,
        _2JN = 64,
        _3JN = 65,
        JUD = 66,
        REV = 67
    }

    public static (int bookId, int chapter, int verse) TranslateVerseId(int verseId)
    {
        int verse = verseId % 1000;
        int chapter = verseId / 1000 % 1000;
        int bookId = verseId / 1000000 % 1000;

        return (bookId, chapter, verse);
    }

    public static int LowerBoundOfBook(int bookId)
    {
        return (bookId * 1000000) + 1000000000;
    }

    public static int UpperBoundOfBook(int bookId)
    {
        return (bookId * 1000000) + 1000999999;
    }

    public static BookCode BookIdToCode(int bookId)
    {
        return (BookCode)bookId;
    }
}

public class BookCodeConverter : JsonConverter<BibleUtilities.BookCode>
{
    public override BibleUtilities.BookCode Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null)
        {
            return BibleUtilities.BookCode.None;
        }
        return (BibleUtilities.BookCode)Enum.Parse(typeof(BibleUtilities.BookCode), value);
    }

    public override void Write(Utf8JsonWriter writer, BibleUtilities.BookCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().Replace("_", ""));
    }
}