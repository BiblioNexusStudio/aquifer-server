namespace Aquifer.Well.API.Endpoints.Languages.List;

public record ListLanguagesResponse
{
    public int Id { get; init; }
    public string Code { get; init; } = null!;
    public string EnglishDisplay { get; init; } = null!;
    public string LocalizedDisplay { get; init; } = null!;
    public string ScriptDirection { get; init; } = null!;
}