namespace Aquifer.API.Endpoints.AI.Translate;

public class Response
{
    public required string Content { get; set; }
    public string? Error { get; set; }
}