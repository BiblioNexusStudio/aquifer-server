namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Response
{
    public required IReadOnlyList<VerseMapping> VerseMappings { get; set; }
}

public class VerseReference
{
    public required int VerseId { get; set; }
    public required string Book { get; set; }
    public required int Chapter { get; set; }
    public required int Verse { get; set; }
}

public class VerseMapping
{
    public required VerseReference TargetVerse { get; set; }
    public required VerseReference SourceVerse { get; set; }
}