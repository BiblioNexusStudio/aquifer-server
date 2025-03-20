namespace Aquifer.API.Endpoints.Bibles.Book.List;

public class Response
{
    public required int Number { get; set; }
    public required string Code { get; set; }
    public required string LocalizedName { get; set; }
    public required int TotalChapters { get; set; }
    public required IReadOnlyList<ResponseChapter> Chapters { get; set; }
}

public class ResponseChapter
{
    public required int Number { get; set; }
    public required int TotalVerses { get; set; }
}