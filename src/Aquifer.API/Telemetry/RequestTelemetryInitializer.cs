using Aquifer.Common;
using Aquifer.Common.Services.Caching;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Aquifer.API.Telemetry;

public class RequestTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : ITelemetryInitializer
{
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not RequestTelemetry requestTelemetry)
        {
            return;
        }

        // Should change this to "internal-admin" once a Well API is separated out
        requestTelemetry.Properties[Constants.TelemetryBnApiPropertyName] = "internal";

        if (httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated is true)
        {
            requestTelemetry.Properties[Constants.TelemetryUserPropertyName] =
                httpContextAccessor.HttpContext.User.FindFirst("user")?.Value;
        }

        if ((httpContextAccessor.HttpContext?.Items.TryGetValue(Constants.HttpContextItemCachedApiKey, out var maybeCachedApiKey) ??
                false) &&
            maybeCachedApiKey is ApiKey cachedApiKey)
        {
            requestTelemetry.Properties[Constants.TelemetryBnApiCallerIdPropertyName] = cachedApiKey.Id.ToString();
            requestTelemetry.Properties[Constants.TelemetryBnApiCallerPropertyName] = cachedApiKey.Organization;
        }
    }
}