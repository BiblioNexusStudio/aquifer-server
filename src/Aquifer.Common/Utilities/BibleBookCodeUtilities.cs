using Aquifer.Common.Extensions;
using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class BibleBookCodeUtilities
{
    private static readonly Dictionary<string, BibleBookMetadata> s_bookCodeToMetadata = new(StringComparer.InvariantCultureIgnoreCase);
    private static readonly Dictionary<BookId, BibleBookMetadata> s_bookIdToMetadata = [];
    private static readonly Dictionary<string, BibleBookMetadata> s_bookFullNameToMetadata = new(StringComparer.InvariantCultureIgnoreCase);

    static BibleBookCodeUtilities()
    {
        var bookIds = Enum.GetValues<BookId>();

        foreach (var bookId in bookIds)
        {
            var metadata = new BibleBookMetadata
            {
                BookId = bookId,
                BookCode = bookId.ToString().Replace("Book", ""),
                BookFullName = bookId.GetDisplayName(),
            };

            s_bookCodeToMetadata.Add(bookId.ToString().Replace("Book", ""), metadata);
            s_bookIdToMetadata.Add(bookId, metadata);
            s_bookFullNameToMetadata.Add(metadata.BookFullName, metadata);
        }
    }

    public static string CodeFromId(BookId bookId)
    {
        return s_bookIdToMetadata.TryGetValue(bookId, out var metadata) ? metadata.BookCode : "";
    }

    public static BookId IdFromCode(string code)
    {
        return s_bookCodeToMetadata.TryGetValue(code, out var metadata) ? metadata.BookId : BookId.None;
    }

    public static string FullNameFromId(BookId bookId)
    {
        return s_bookIdToMetadata.TryGetValue(bookId, out var metadata) ? metadata.BookFullName : "";
    }

    public static BookId IdFromFullName(string fullName)
    {
        return s_bookFullNameToMetadata.TryGetValue(fullName, out var metadata) ? metadata.BookId : BookId.None;
    }

    public static List<BibleBookMetadata> GetAll()
    {
        return s_bookIdToMetadata.Select(x => x.Value).Skip(1).ToList();
    }

    public static bool TryCastBookId(int value, out BookId bookId)
    {
        if (Enum.IsDefined(typeof(BookId), value))
        {
            bookId = (BookId)value;
            return true;
        }

        bookId = BookId.None;
        return false;
    }

    public record BibleBookMetadata
    {
        public BookId BookId { get; set; }
        public string BookCode { get; set; } = null!;
        public string BookFullName { get; set; } = null!;
    }
}