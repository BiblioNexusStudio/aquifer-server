namespace Aquifer.Public.API.Endpoints.Resources.Collections.Get;

public sealed class Request
{
    /// <summary>
    /// The collection code of the collection to retrieve.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Optional languages to search on by internal Aquifer language id. If no languages are specified then all languages will be returned. Only one of language ids or codes may be provided.
    /// </summary>
    public IReadOnlyList<int>? LanguageIds { get; init; }

    /// <summary>
    /// Optional languages to search on by ISO 639-3 code that is 3 characters in length (e.g. "eng"). Only one of language ids or codes may be provided.
    /// </summary>
    public IReadOnlyList<string>? LanguageCodes { get; init; }
}