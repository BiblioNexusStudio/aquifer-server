using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using FastEndpoints;

namespace Aquifer.API.Helpers;

public static class EndpointHelpers
{
    public const int OneDayInSeconds = 86400;
    public const int OneHourInSeconds = 3600;
    public const int TenMinutesInSeconds = 600;

    public static Action<RouteHandlerBuilder> ServerCacheInSeconds(int seconds)
    {
        return x => x.CacheOutput(c => c.Expire(TimeSpan.FromSeconds(seconds)));
    }

    [DoesNotReturn]
    public static void ThrowEntityNotFoundError<TRequest>(Expression<Func<TRequest, object?>> property)
    {
        ValidationContext<TRequest>.Instance.ThrowError(property, "No record found.");
    }

    public static void ThrowErrorIfNull<TRequest>(object? value,
        Expression<Func<TRequest, object?>> property,
        string errorMessage,
        int? statusCode = null)
    {
        if (value is null)
        {
            ValidationContext<TRequest>.Instance.ThrowError(property, errorMessage, statusCode);
        }
    }
}