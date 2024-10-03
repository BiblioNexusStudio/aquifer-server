using System.Text.Json.Serialization;

namespace Aquifer.Public.API.Endpoints.Bibles.Alignments.Greek;

public sealed class Response
{
    public required int BibleId { get; init; }
    public required string BibleName { get; init; }
    public required string BibleAbbreviation { get; init; }
    public required string GreekBibleAbbreviation { get; init; }
    public required string BookName { get; init; }
    public required string BookCode { get; init; }
    public required IReadOnlyList<ResponseChapter> Chapters { get; init; }
}

public sealed class ResponseChapter
{
    public required int Number { get; init; }
    public required IReadOnlyList<ResponseChapterVerse> Verses { get; init; }
}

public sealed class ResponseChapterVerse
{
    public required int Number { get; init; }
    public required IReadOnlyList<ResponseWordWithGreekAlignment> Words { get; init; }
}

public sealed class ResponseWordWithGreekAlignment
{
    public required int Number { get; init; }
    public required string Word { get; init; }
    public required bool NextWordIsInGroup { get; init; }
    public required IReadOnlyList<ResponseGreekWord> GreekWords { get; init; }
}

public sealed class ResponseGreekWord
{
    public required string Word { get; init; }
    public required string GrammarType { get; init; }
    public required string UsageCode { get; init; }
    public required string Lemma { get; init; }
    public required string StrongsNumber { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public required IReadOnlyList<ResponseGreekSense>? Senses { get; init; }
}

public sealed class ResponseGreekSense
{
    public required IReadOnlyList<string> Glosses { get; init; }
    public required string Definition { get; init; }
}