namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public record Request
{
    /// <summary>
    ///     Book id based off USFM book numbers. Can get a list of available books and ids from the /bible-books endpoint.
    ///     Use this to search across an entire book, use startVerseId and endVerseId for more narrow results.
    /// </summary>
    public int? BookId { get; init; }

    /// <summary>
    ///     Optional verse id to search on. The Id should be prefixed by a 1, then the book number using the standard USFM number,
    ///     the chapter number, and the verse number. All values should be padded with zeroes to make them length 3.
    ///     For example, If you were looking for Mark chapter 5 verse 13, the id would be 1042005013. You can get a list of book
    ///     numbers from the /bible-books endpoint.
    /// </summary>
    public int? StartVerseId { get; init; }

    /// <summary>
    ///     Optional end verse id. If not provided it will default to the startVerseId (i.e. search according to a single verse)
    /// </summary>
    public int? EndVerseId { get; init; }
}