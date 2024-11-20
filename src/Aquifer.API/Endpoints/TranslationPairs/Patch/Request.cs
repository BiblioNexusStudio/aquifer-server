namespace Aquifer.API.Endpoints.TranslationPairs.Patch;

public class Request
{
    public int Id { get; set; }
    public string? Key { get; set; }
    public string? Value { get; set; }
}