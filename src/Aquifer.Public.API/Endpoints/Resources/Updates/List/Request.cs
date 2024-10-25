using System.ComponentModel;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Request
{
    /// <summary>
    ///     UTC timestamp (e.g. 07/20/2024) indicating how far back to check for updates. The maximum look-back is 90 days.
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    ///     Optional LanguageId to search on. If none is provided, updates for all languages will be returned.
    /// </summary>
    public int? LanguageId { get; init; }

    /// <summary>
    ///     Optional ISO 639-3 code that is 3 characters in length (e.g. "eng").
    /// </summary>
    public string? LanguageCode { get; init; }

    /// <summary>
    ///     The number of results to return. Max is 100.
    /// </summary>
    [DefaultValue(1000)]
    public int Limit { get; init; } = 1000;

    /// <summary>
    ///     Records to skip if paging through results.
    /// </summary>
    [DefaultValue(0)]
    public int Offset { get; init; }

    /// <summary>
    ///     Optional resource collection code to search for. Search by code retrieved from /resources/collections endpoint such
    ///     as "TyndaleBibleDictionary".
    /// </summary>
    public string? ResourceCollectionCode { get; init; }
}