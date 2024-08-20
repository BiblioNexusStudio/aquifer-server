using Aquifer.Common.Services;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.Resources.Get;

public class Endpoint(AquiferDbContext dbContext, IResourceContentRequestTrackingService trackingService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}");
        Description(d => d.ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "Get specific resource information.";
            s.Description =
                "For a given resource id, return the data for that resource. This can be text content as well as CDN links for image, audio, and video media types.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response = await ResourceHelper.GetResourceContentAsync(dbContext,
            new ByLanguage.Request
            {
                ContentId = req.ContentId,
                ContentTextType = req.ContentTextType
            },
            ThrowError,
            ct);
    }

    public override async Task OnAfterHandleAsync(Request req, Response res, CancellationToken ct)
    {
        const string endpointId = "public-resources-get";
        await trackingService.TrackAsync(HttpContext, req.ContentId, endpointId, "public-api");
    }
}