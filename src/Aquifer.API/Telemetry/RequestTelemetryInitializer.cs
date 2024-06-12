using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Aquifer.API.Telemetry;

public class RequestTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : ITelemetryInitializer
{
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is RequestTelemetry requestTelemetry && httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated is true)
        {
            requestTelemetry.Properties["user"] = httpContextAccessor.HttpContext.User.FindFirst("user")?.Value;
        }
    }
}