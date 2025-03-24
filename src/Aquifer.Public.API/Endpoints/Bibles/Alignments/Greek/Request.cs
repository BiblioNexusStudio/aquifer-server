using System.ComponentModel;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

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
    [DefaultValue(1)]
    public int StartChapter { get; init; } = 1;

    /// <summary>
    /// Optional start verse in the start chapter.
    /// </summary>
    [DefaultValue(0)]
    public int StartVerse { get; init; } = 0;

    /// <summary>
    /// Optional start word in the start verse.
    /// </summary>
    [DefaultValue(1)]
    public int StartWord { get; init; } = 1;

    /// <summary>
    /// Optional end chapter in the book.
    /// </summary>
    [DefaultValue(999)]
    public int EndChapter { get; init; } = 999;

    /// <summary>
    /// Optional end verse in the end chapter.
    /// </summary>
    [DefaultValue(999)]
    public int EndVerse { get; init; } = 999;

    /// <summary>
    /// Optional end word in the end verse.
    /// </summary>
    [DefaultValue(999)]
    public int EndWord { get; init; } = 999;

    /// <summary>
    /// Optional. If <c>true</c> then any available Greek word sense information for each Greek word (definition and glosses) will be included in the response.
    /// </summary>
    [DefaultValue(false)]
    public bool ShouldReturnSenseData { get; init; }
}