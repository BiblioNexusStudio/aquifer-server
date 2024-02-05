using Aquifer.Common.Extensions;
using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class BibleBookUtilities
{
    private static readonly Dictionary<string, BibleBookMetadata> BookCodeToMetadata = [];
    private static readonly Dictionary<BookId, BibleBookMetadata> BookIdToMetadata = [];

    static BibleBookUtilities()
    {
        var bookIds = Enum.GetValues(typeof(BookId)).Cast<BookId>();

        foreach (var bookId in bookIds)
        {
            var metadata = new BibleBookMetadata
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

    public static List<BibleBookMetadata> GetAll()
    {
        return BookIdToMetadata.Select(x => x.Value).Skip(1).ToList();
    }

    public record BibleBookMetadata
    {
        public BookId BookId { get; set; }
        public string BookCode { get; set; } = null!;
        public string BookFullName { get; set; } = null!;
    }
}