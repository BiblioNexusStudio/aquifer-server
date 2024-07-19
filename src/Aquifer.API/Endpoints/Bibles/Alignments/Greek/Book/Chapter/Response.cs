namespace Aquifer.API.Endpoints.Bibles.Alignments.Greek.Book.Chapter;

public record Response
{
    public int Verse { get; set; }
    public IEnumerable<EnglishWordWithGreekAlignment> Words { get; set; } = [];
}

public record EnglishWordWithGreekAlignment
{
    public string Word { get; set; } = null!;
    public bool NextWordIsInGroup { get; set; }
    public IEnumerable<GreekWord> GreekWords { get; set; } = [];
}

public record GreekWord
{
    public string Word { get; set; } = null!;
    public IEnumerable<GreekSense> Senses { get; set; } = [];
}

public record GreekSense
{
    public IEnumerable<string> Glosses { get; set; } = [];
    public string Definition { get; set; } = null!;
}