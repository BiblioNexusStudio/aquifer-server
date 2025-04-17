namespace Aquifer.Well.API.Helpers;

public static class EndpointHelpers
{
    public const int OneHourInSeconds = 3600;
    public const int TenMinutesInSeconds = 600;

    private const string AnonymousOutputCacheTag = "anonymous";

    public static Action<RouteHandlerBuilder> ServerCacheInSeconds(int seconds)
    {
        return x => x.CacheOutput(
            c => c
                .Expire(TimeSpan.FromSeconds(seconds))
                .Tag(AnonymousOutputCacheTag));
    }
}