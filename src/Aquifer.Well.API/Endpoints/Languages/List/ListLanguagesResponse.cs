namespace Aquifer.Well.API.Endpoints.Languages.List;

public record ListLanguagesResponse
{
    public required int Id { get; init; }
    public required string Code { get; init; }
    public required string EnglishDisplay { get; init; }
    public required string LocalizedDisplay { get; init; }
    public required string ScriptDirection { get; init; }
}