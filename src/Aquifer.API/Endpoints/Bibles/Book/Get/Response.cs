namespace Aquifer.API.Endpoints.Bibles.Book.Get;

public record Response
{
    public required string BookCode { get; set; }
    public required string DisplayName { get; set; }
    public required int TextSize { get; set; }
    public required int AudioSize { get; set; }
    public required int ChapterCount { get; set; }
    public required string TextUrl { get; set; }
    public required object? AudioUrls { get; set; }
}