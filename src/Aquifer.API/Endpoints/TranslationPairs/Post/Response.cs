namespace Aquifer.API.Endpoints.TranslationPairs.Post;

public class Response
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string LanguageEnglishDisplay { get; set; } = null!;
}