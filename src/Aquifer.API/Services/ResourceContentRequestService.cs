namespace Aquifer.API.Services;

public interface IResourceContentRequestService
{
    public void TrackResourceContentRequestsInBackground(int[] resourceContentIds);
}

public class ResourceContentRequestService(ResourceContentRequestBackgroundService _backgroundService,
    IHttpContextAccessor _httpContextAccessor) : IResourceContentRequestService
{
    public void TrackResourceContentRequestsInBackground(int[] resourceContentIds)
    {
        _backgroundService.TrackResourceContentRequestsInBackground(resourceContentIds,
            _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown");
    }
}