using System.ComponentModel;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Request
{
    /// <summary>
    /// UTC timestamp (e.g. 07/20/2024) representing the starting time of a range in which to look for new updates. This is required when not
    /// passing the deprecated `Timestamp` property.
    /// </summary>
    public DateTime? StartTimestamp { get; set; }

    /// <summary>
    /// UTC timestamp (e.g. 07/20/2024) representing the ending time of a range in which to look for new updates. If no value is provided it
    /// will default to UtcNow.
    /// </summary>
    [DefaultValue(typeof(DateTime), "DateTime.UtcNow")]
    public DateTime? EndTimestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Optional LanguageId to search on. If none is provided, updates for all languages will be returned.
    /// </summary>
    public int? LanguageId { get; init; }

    /// <summary>
    /// Optional ISO 639-3 code that is 3 characters in length (e.g. "eng").
    /// </summary>
    public string? LanguageCode { get; init; }

    /// <summary>
    /// The number of results to return. Max is 1000.
    /// </summary>
    [DefaultValue(1000)]
    public int Limit { get; init; } = 1000;

    /// <summary>
    /// Records to skip if paging through results.
    /// </summary>
    [DefaultValue(0)]
    public int Offset { get; init; }

    /// <summary>
    /// Optional resource collection code to search for. Search by code retrieved from /resources/collections endpoint such
    /// as "TyndaleBibleDictionary".
    /// </summary>
    public string? ResourceCollectionCode { get; init; }

    /// <summary>
    /// WARNING: DEPRECATED!  Use `StartTimestamp` instead.  If `StartTimestamp` is provided then this value will be ignored.
    /// </summary>
    [Obsolete("Use StartTimestamp instead.")]
    public DateTime? Timestamp { get; init; }
}