namespace Aquifer.Public.API.Endpoints.Resources.Updated.List;

public class Request
{
    /// <summary>
    ///     UTC timestamp (e.g. 07/20/2024) indicating how far back to check for updates. The maximum look-back is 90 days.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Optional LanguageId to search on. If none is provided, updates for all languages will be returned.
    /// </summary>
    public int? LanguageId { get; set; }
}