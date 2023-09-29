namespace Aquifer.Data.Enums;

public enum BookId
{
    None = 0,
    BookGEN = 1,
    BookEXO = 2,
    BookLEV = 3,
    BookNUM = 4,
    BookDEU = 5,
    BookJOS = 6,
    BookJDG = 7,
    BookRUT = 8,
    Book1SA = 9,
    Book2SA = 10,
    Book1KI = 11,
    Book2KI = 12,
    Book1CH = 13,
    Book2CH = 14,
    BookEZR = 15,
    BookNEH = 16,
    BookEST = 17,
    BookJOB = 18,
    BookPSA = 19,
    BookPRO = 20,
    BookECC = 21,
    BookSNG = 22,
    BookISA = 23,
    BookJER = 24,
    BookLAM = 25,
    BookEZK = 26,
    BookDAN = 27,
    BookHOS = 28,
    BookJOL = 29,
    BookAMO = 30,
    BookOBA = 31,
    BookJON = 32,
    BookMIC = 33,
    BookNAM = 34,
    BookHAB = 35,
    BookZEP = 36,
    BookHAG = 37,
    BookZEC = 38,
    BookMAL = 39,
    BookMAT = 41,
    BookMRK = 42,
    BookLUK = 43,
    BookJHN = 44,
    BookACT = 45,
    BookROM = 46,
    Book1CO = 47,
    Book2CO = 48,
    BookGAL = 49,
    BookEPH = 50,
    BookPHP = 51,
    BookCOL = 52,
    Book1TH = 53,
    Book2TH = 54,
    Book1TI = 55,
    Book2TI = 56,
    BookTIT = 57,
    BookPHM = 58,
    BookHEB = 59,
    BookJAS = 60,
    Book1PE = 61,
    Book2PE = 62,
    Book1JN = 63,
    Book2JN = 64,
    Book3JN = 65,
    BookJUD = 66,
    BookREV = 67
}

public static class BookIdSerializer
{
    public static string ToCode(this BookId id)
    {
        return id.ToString().Replace("Book", "");
    }

    public static BookId FromCode(string code)
    {
        return Enum.TryParse($"Book{code}", out BookId result) ? result : BookId.None;
    }
}