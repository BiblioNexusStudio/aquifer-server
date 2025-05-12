using Aquifer.Well.API.Endpoints.PushNotifications.Models;
using Aquifer.Well.API.Services;
using FastEndpoints;

namespace Aquifer.Well.API.Endpoints.PushNotifications.Requests;

public class NotificationRequestsEndpoint(INotificationService notificationService) : Endpoint<NotificationRequest>
{
    public override void Configure()
    {
        Post("/push-notifications/requests");
    }

    public override async Task HandleAsync(NotificationRequest req, CancellationToken ct)
    {
        if ((req.Silent && string.IsNullOrWhiteSpace(req?.Action)) ||
            (!req.Silent && !string.IsNullOrWhiteSpace(req?.Text)))
        {
            await SendAsync("Bad notification request.",422, ct);
        }
        
        var success = await notificationService.RequestNotificationAsync(req!, ct);

        if (!success)
        {
            await SendAsync("Notification request failed.",422, ct);
            return;
        }
        
        await SendOkAsync(ct);
    }
}
