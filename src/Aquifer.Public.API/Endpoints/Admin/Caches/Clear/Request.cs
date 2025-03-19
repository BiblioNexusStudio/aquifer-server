namespace Aquifer.Public.API.Endpoints.Admin.Caches.Clear;

public sealed class Request
{
    public bool ShouldClearMemoryCache { get; set; }
    public bool ShouldClearOutputCache { get; set; }
}