using Aquifer.Common;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Aquifer.Well.API.Telemetry;

public class RequestTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : ITelemetryInitializer
{
    private const string ApiIdentifier = "well";
    private const string BnUserIdHeaderKey = "bn-user-id";

    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not RequestTelemetry requestTelemetry)
        {
            return;
        }

        requestTelemetry.Properties[Constants.TelemetryBnApiPropertyName] = ApiIdentifier;

        if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(BnUserIdHeaderKey, out var userId) == true)
        {
            requestTelemetry.Properties[Constants.TelemetryAppInsightsUserIdPropertyName] = userId.FirstOrDefault();
        }
    }
}