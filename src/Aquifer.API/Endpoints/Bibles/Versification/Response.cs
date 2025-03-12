namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Response
{
    public IEnumerable<VerseMapping> VerseMappings { get; set; } = [];
}

public class VerseReference
{
    public required string BookName { get; set; }
    public required int ChapterNumber { get; set; }
    public required int VerseNumber { get; set; }
}

public class VerseMapping
{
    public required VerseReference TargetBibleVerse { get; set; }
    public required VerseReference SourceBibleVerse { get; set; }
}