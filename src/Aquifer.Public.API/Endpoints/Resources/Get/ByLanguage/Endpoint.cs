using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.Resources.Get.ByLanguage;

public class Endpoint(AquiferDbReadOnlyContext dbContext, IResourceContentRequestTrackingMessagePublisher trackingMessagePublisher)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}/by-language/{LanguageCode}");
        Description(d => d
            .WithTags("Resources")
            .ProducesProblemFE()
            .ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "Get specific resource information by a different language.";
            s.Description = "For a given resource id, return the data for that resource in the alternative language.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response = await ResourceHelper.GetResourceContentAsync(dbContext,
            new CommonResourceRequest(req.ContentId, req.ContentTextType, req.LanguageCode),
            ThrowError,
            ct);
    }

    public override async Task OnAfterHandleAsync(Request req, Response res, CancellationToken ct)
    {
        const string endpointId = "public-resources-get-by-language";
        await trackingMessagePublisher.PublishTrackResourceContentRequestMessageAsync(HttpContext, res.Id, endpointId, source: "public-api", ct);
    }
}