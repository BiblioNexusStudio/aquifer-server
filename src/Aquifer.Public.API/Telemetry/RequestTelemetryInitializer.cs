using Aquifer.Common;
using Aquifer.Common.Services.Caching;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Aquifer.Public.API.Telemetry;

public class RequestTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : ITelemetryInitializer
{
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not RequestTelemetry requestTelemetry)
        {
            return;
        }

        requestTelemetry.Properties[Constants.TelemetryBnApiPropertyName] = "public";

        if ((httpContextAccessor.HttpContext?.Items.TryGetValue(Constants.HttpContextItemCachedApiKey, out var maybeCachedApiKey) ??
                false) &&
            maybeCachedApiKey is CachedApiKey cachedApiKey)
        {
            requestTelemetry.Properties[Constants.TelemetryBnApiCallerIdPropertyName] = cachedApiKey.Id.ToString();
            requestTelemetry.Properties[Constants.TelemetryBnApiCallerPropertyName] = cachedApiKey.Organization;
        }
    }
}