namespace Aquifer.Public.API.Helpers;

public static class EndpointHelpers
{
    public static Action<RouteHandlerBuilder> SetCacheOption(int minutes = 5)
    {
        return x => x.CacheOutput(c => c.Expire(TimeSpan.FromMinutes(minutes)));
    }
}