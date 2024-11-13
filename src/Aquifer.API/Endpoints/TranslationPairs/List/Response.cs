namespace Aquifer.API.Endpoints.TranslationPairs.List;

public class Response
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}