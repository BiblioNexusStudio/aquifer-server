namespace Aquifer.Public.API.Endpoints.Resources.Get.AvailableLanguages;

public class Request
{
    /// <summary>
    ///     The resource content id to check additional languages for
    /// </summary>
    public int ContentId { get; init; }
}