namespace Aquifer.API.Endpoints.AI.Translate;

public class Request
{
    public string LanguageName { get; set; } = null!;
    public string? LanguageCode { get; set; }
    public string Content { get; set; } = null!;
    public string? Prompt { get; set; }
}