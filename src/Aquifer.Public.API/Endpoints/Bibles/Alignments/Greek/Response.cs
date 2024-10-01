using System.Text.Json.Serialization;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

public class Response
{
    public required int BibleId { get; init; }
    public required string BibleName { get; init; }
    public required string BibleAbbreviation { get; init; }
    public required string GreekBibleAbbreviation { get; init; }
    public required string BookName { get; init; }
    public required string BookCode { get; init; }
    public required IReadOnlyList<ResponseChapter> Chapters { get; init; }
}

public class ResponseChapter
{
    public required int Number { get; init; }
    public required IReadOnlyList<ResponseChapterVerse> Verses { get; init; }
}

public class ResponseChapterVerse
{
    public required int Number { get; init; }
    public required IReadOnlyList<EnglishWordWithGreekAlignment> Words { get; init; }
}

public class EnglishWordWithGreekAlignment
{
    public required int Number { get; init; }
    public required string Word { get; init; }
    public required bool NextWordIsInGroup { get; init; }
    public required IReadOnlyList<GreekWord> GreekWords { get; init; }
}

public class GreekWord
{
    public required string Word { get; init; }
    public required string GrammarType { get; init; }
    public required string UsageCode { get; init; }
    public required string Lemma { get; init; }
    public required string StrongsNumber { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public required IReadOnlyList<GreekSense>? Senses { get; init; }
}

public class GreekSense
{
    public required IReadOnlyList<string> Glosses { get; init; }
    public required string Definition { get; init; }
}