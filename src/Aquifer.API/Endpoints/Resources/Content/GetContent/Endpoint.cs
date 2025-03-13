using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Schemas;
using FastEndpoints;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.GetContent;

public class Endpoint(
    AquiferDbContext dbContext,
    TelemetryClient telemetry,
    IResourceContentRequestTrackingMessagePublisher trackingMessagePublisher)
    : Endpoint<Request, object>
{
    public override void Configure()
    {
        // This was originally a Module that was moved to use FastEndpoints. Unfortunately, it shared a very similar
        // path as /Get. We should probably revisit these endpoints, either combine them or establish a real difference
        // that can guide the path. This endpoint is used by Bible Well, and the other one is used by the CMS.
        Get("/resources/{ResourceContentId}/content");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var contentVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == req.ResourceContentId && rcv.IsPublished)
            .Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (contentVersion == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Text)
        {
            await SendOkAsync(JsonUtilities.DefaultDeserialize(contentVersion.Content), ct);
            return;
        }

        string? url = null;

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Audio)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(contentVersion.Content);
            if (req.AudioType == "webm" && deserialized.Webm != null)
            {
                url = deserialized.Webm.Url;
            }
            else if (deserialized.Mp3 != null)
            {
                url = deserialized.Mp3.Url;
            }
        }
        else
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentUrlJsonSchema>(contentVersion.Content);
            url = deserialized.Url;
        }

        if (url != null)
        {
            await SendRedirectAsync(url, false, true);
            return;
        }

        telemetry.TrackTrace($"Content with ID {req.ResourceContentId} exists but has unexpected `Content` JSON.", SeverityLevel.Warning);
        await SendNotFoundAsync(ct);
    }

    public override async Task OnAfterHandleAsync(Request req, object res, CancellationToken ct)
    {
        const string endpointId = "resources-content-getcontent";
        await trackingMessagePublisher.PublishTrackResourceContentRequestMessageAsync(HttpContext, req.ResourceContentId, endpointId, source: null, ct);
    }
}