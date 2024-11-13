namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Request
{
    public int LanguageId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}