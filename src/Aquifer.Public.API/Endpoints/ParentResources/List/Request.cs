namespace Aquifer.Public.API.Endpoints.ParentResources.List;

public sealed class Request
{
    /// <summary>
    /// Optional language to search on by internal Aquifer language id. If no language is specified then English will be used by default. Only one of language id or code may be provided.
    /// </summary>
    public int? LanguageId { get; init; }

    /// <summary>
    /// Optional language to search on by ISO 639-3 code that is 3 characters in length (e.g. "eng"). Only one of language id or code may be provided.
    /// </summary>
    public string? LanguageCode { get; init; }
}