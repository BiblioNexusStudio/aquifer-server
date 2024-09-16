namespace Aquifer.Public.API.Endpoints.Bibles.List;

public class Request
{
    /// <summary>
    /// Optional language to search on by internal Aquifer language ID. If no language is specified then Bibles for all languages will be returned. Only one of language ID or code may be provided.
    /// </summary>
    public int? LanguageId { get; init; }

    /// <summary>
    /// Optional language to search on by ISO 639-3 code that is 3 characters in length (e.g. "eng"). If no language value is specified then Bibles for all languages will be returned. Only one of language ID or code may be provided.
    /// </summary>
    public string? LanguageCode { get; init; }

    /// <summary>
    /// If specified then only language default Bibles (for <c>true</c>) or language non-default Bibles (for <c>false</c>) will be returned. If no value is provided then all Bibles will be returned.
    /// </summary>
    public bool? IsLanguageDefault { get; init; }

    /// <summary>
    /// If specified then only Bibles that have Greek alignment (for <c>true</c>) or that don't have a Greek alignment (for <c>false</c>) will be returned. If no value is provided then all Bibles will be returned.
    /// </summary>
    public bool? HasGreekAlignment { get; init; }

    /// <summary>
    /// If specified then only Bibles that have audio files (for <c>true</c>) or that don't have audio files (for <c>false</c>) will be returned. If no value is provided then all Bibles will be returned.
    /// </summary>
    public bool? HasAudio { get; init; }
}