namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Request
{
    /// <summary>
    /// The ID of the Bible for which to return text.
    /// </summary>
    public int BibleId { get; init; }

    /// <summary>
    /// Book code based off USFM book identifier (e.g. GEN, EXO, etc.). Can get a list of available books and identifiers
    /// from the /bible-books endpoint. This parameter is required.
    /// </summary>
    public string? BookCode { get; init; }

    /// <summary>
    /// Optional start chapter in the book. Required if start verse is specified.
    /// </summary>
    public int? StartChapter { get; init; }

    /// <summary>
    /// Optional start verse in the start chapter. Must also provide a start chapter if specified.
    /// </summary>
    public int? StartVerse { get; init; }

    /// <summary>
    /// Optional end chapter in the book. Required if end verse is specified.
    /// </summary>
    public int? EndChapter { get; init; }

    /// <summary>
    /// Optional end verse in the end chapter. Must also provide an end chapter if specified.
    /// </summary>
    public int? EndVerse { get; init; }
}