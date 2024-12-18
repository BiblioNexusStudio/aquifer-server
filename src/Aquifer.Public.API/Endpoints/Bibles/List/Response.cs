namespace Aquifer.Public.API.Endpoints.Bibles.List;

public record Response
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Abbreviation { get; init; }
    public required int LanguageId { get; init; }
    public required bool IsLanguageDefault { get; init; }
    public required bool HasAudio { get; init; }
    public required bool HasGreekAlignment { get; init; }
    public required BibleLicenseInfo LicenseInfo { get; init; }
}