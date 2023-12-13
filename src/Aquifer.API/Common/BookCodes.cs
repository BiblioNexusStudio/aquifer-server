using Aquifer.Data.Enums;

namespace Aquifer.API.Common;

public static class BookCodes
{
    private record BookMetadata
    {
        public BookId BookId { get; set; }
        public string BookCode { get; set; } = null!;
    }

    private static readonly Dictionary<string, BookMetadata> BookCodeToMetadata;
    private static readonly Dictionary<BookId, BookMetadata> BookEnumToMetadata;

    static BookCodes()
    {
        var bookIdEnums = Enum.GetValues(typeof(BookId)).Cast<BookId>();
        BookCodeToMetadata = bookIdEnums.ToDictionary(bc => bc.ToString().Replace("Book", ""), bc => new BookMetadata
        {
            BookId = bc,
            BookCode = bc.ToString().Replace("Book", "")
        });
        BookEnumToMetadata = bookIdEnums.ToDictionary(bc => bc, bc => new BookMetadata
        {
            BookId = bc,
            BookCode = bc.ToString().Replace("Book", "")
        });
    }

    public static string CodeFromEnum(BookId bookId)
    {
        return BookEnumToMetadata.TryGetValue(bookId, out var obj) ? obj.BookCode : "";
    }

    public static BookId EnumFromCode(string stringValue)
    {
        return BookCodeToMetadata.TryGetValue(stringValue, out var obj) ? obj.BookId : BookId.None;
    }
}