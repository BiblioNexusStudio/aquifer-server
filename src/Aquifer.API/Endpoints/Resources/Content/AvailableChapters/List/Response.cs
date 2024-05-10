namespace Aquifer.API.Endpoints.Resources.Content.AvailableChapters.List;

public record Response
{
    public required string BookCode { get; set; }
    public required IEnumerable<int> Chapters { get; set; }
}