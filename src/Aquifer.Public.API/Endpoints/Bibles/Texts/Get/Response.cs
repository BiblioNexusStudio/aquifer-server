using System.Text.Json.Serialization;

namespace Aquifer.Public.API.Endpoints.Bibles.Texts.Get;

public class Response
{
    public required int BibleId { get; set; }
    public required string BibleName { get; set; }
    public required string BibleAbbreviation { get; set; }
    public required string BookName { get; set; }
    public required string BookCode { get; set; }
    public required IReadOnlyList<ResponseChapter> Chapters { get; set; }
}

public class ResponseChapter
{
    public required int Number { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public required ResponseChapterAudio? Audio { get; set; }

    public required IReadOnlyList<ResponseChapterVerse> Verses { get; set; }
}

public class ResponseChapterAudio
{
    public required ResponseAudioFile? Webm { get; set; }
    public required ResponseAudioFile? Mp3 { get; set; }
}

public class ResponseAudioFile
{
    public required string Url { get; set; }
    public required int Size { get; set; }
}

public class ResponseChapterVerse
{
    public required int Number { get; set; }
    public required string Text { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public required ResponseChapterVerseAudioTimestamp? AudioTimestamp { get; set; }
}

public class ResponseChapterVerseAudioTimestamp
{
    public required decimal Start { get; set; }
    public required decimal End { get; set; }
}