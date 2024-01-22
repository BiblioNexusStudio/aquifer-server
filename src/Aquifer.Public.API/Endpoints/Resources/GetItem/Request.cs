namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public record Request
{
    /// <summary>
    ///     The resource id to return content for
    /// </summary>
    public int ContentId { get; init; }
}