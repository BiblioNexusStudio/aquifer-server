using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Batch.Text;

public class Endpoint(
    AquiferDbContext dbContext,
    TelemetryClient telemetry,
    IResourceContentRequestTrackingMessagePublisher trackingMessagePublisher)
    : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/batch/text", "/resources/batch/content/text");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var contents = await dbContext.ResourceContentVersions
            .Where(rcv => req.Ids.Contains(rcv.ResourceContentId) &&
                rcv.IsPublished &&
                rcv.ResourceContent.MediaType == ResourceContentMediaType.Text)
            .Select(contentVersion => new Response
            {
                Id = contentVersion.ResourceContentId,
                Content = JsonUtilities.DefaultDeserialize(contentVersion.Content)
            })
            .ToListAsync(ct);

        if (contents.Count != req.Ids.Count)
        {
            telemetry.TrackTrace("IDs and content found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", req.Ids) } });
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(contents, ct);
    }

    public override async Task OnAfterHandleAsync(Request req, List<Response> res, CancellationToken ct)
    {
        const string endpointId = "resources-content-batch-text";
        await trackingMessagePublisher.TrackAsync(HttpContext, req.Ids, endpointId, cancellationToken: ct);
    }
}