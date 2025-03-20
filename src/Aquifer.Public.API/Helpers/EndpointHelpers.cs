namespace Aquifer.Public.API.Helpers;

public static class EndpointHelpers
{
    public const int OneHourInSeconds = 3600;
    public const int TenMinutesInSeconds = 600;

    public const string AnonymousOutputCacheTag = "anonymous";

    // Because CacheOutput ignores requests that include an Authorization header, this is only meant for use on AllowAnonymous() endpoints.
    public static Action<RouteHandlerBuilder> UnauthenticatedServerCacheInSeconds(int seconds)
    {
        return x => x.CacheOutput(
            c => c
                .Expire(TimeSpan.FromSeconds(seconds))
                .Tag(AnonymousOutputCacheTag));
    }

    public static class EndpointTags
    {
        public const string ExcludeFromSwaggerDocument = nameof(ExcludeFromSwaggerDocument);
    }
}