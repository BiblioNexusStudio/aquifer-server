using Aquifer.Data.Enums;

namespace Aquifer.API.Common;

public static class BookCodes
{
    private static readonly Dictionary<string, BookMetadata> BookCodeToMetadata = [];
    private static readonly Dictionary<BookId, BookMetadata> BookIdToMetadata = [];

    static BookCodes()
    {
        var bookIds = Enum.GetValues(typeof(BookId)).Cast<BookId>();

        foreach (var bookId in bookIds)
        {
            var metadata = new BookMetadata
            {
                BookId = bookId,
                BookCode = bookId.ToString().Replace("Book", ""),
                BookFullName = bookId.GetDisplayName()
            };
            BookCodeToMetadata.Add(bookId.ToString().Replace("Book", ""), metadata);
            BookIdToMetadata.Add(bookId, metadata);
        }
    }

    public static string CodeFromId(BookId bookId)
    {
        return BookIdToMetadata.TryGetValue(bookId, out var obj) ? obj.BookCode : "";
    }

    public static BookId IdFromCode(string stringValue)
    {
        return BookCodeToMetadata.TryGetValue(stringValue, out var obj) ? obj.BookId : BookId.None;
    }

    public static string FullNameFromId(BookId bookId)
    {
        return BookIdToMetadata.TryGetValue(bookId, out var obj) ? obj.BookFullName : "";
    }

    private record BookMetadata
    {
        public BookId BookId { get; set; }
        public string BookCode { get; set; } = null!;
        public string BookFullName { get; set; } = null!;
    }
}