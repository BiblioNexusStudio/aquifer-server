namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Request
{
    /// <summary>
    /// The id of the Bible for which to return text.
    /// </summary>
    public int BibleId { get; init; }

    /// <summary>
    /// Book code based off USFM book identifier (e.g. GEN, EXO, etc.). Can get a list of available books and identifiers
    /// from the /bibles/books endpoint. This parameter is required.
    /// </summary>
    public string BookCode { get; init; } = null!;

    /// <summary>
    /// Optional start chapter in the book.
    /// </summary>
    public int StartChapter { get; init; } = 1;

    /// <summary>
    /// Optional start verse in the start chapter.
    /// </summary>
    public int StartVerse { get; init; } = 1;

    /// <summary>
    /// Optional end chapter in the book.
    /// </summary>
    public int EndChapter { get; init; } = 999;

    /// <summary>
    /// Optional end verse in the end chapter.
    /// </summary>
    public int EndVerse { get; init; } = 999;

    /// <summary>
    /// If <c>true</c> then any available audio information for each chapter and verse will be included in the response.
    /// </summary>
    public bool ShouldReturnAudioData { get; init; }
}