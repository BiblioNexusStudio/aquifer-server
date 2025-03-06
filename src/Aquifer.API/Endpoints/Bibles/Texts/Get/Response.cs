namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Response
{
    public required int BibleId { get; set; }
    public required string BibleName { get; set; }
    public required string BibleAbbreviation { get; set; }
    public required string BookName { get; set; }
    public required string BookCode { get; set; }
    public required int BookNumber { get; set; }
    public IEnumerable<ResponseChapters> Chapters { get; set; } = null!;
}

public class ResponseChapters
{
    public required int Number { get; set; }
    public required IEnumerable<ResponseChapterVerses> Verses { get; set; }
}

public class ResponseChapterVerses
{
    public required int Number { get; set; }
    public required string Text { get; set; }
    public VerseReference? SourceTextVerseReference { get; set; }
}

public class VerseReference
{
    public required string BookName { get; set; }
    public required int ChapterNumber { get; set; }
    public required int VerseNumber { get; set; }
}