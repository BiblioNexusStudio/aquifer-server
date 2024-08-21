using Aquifer.Data;
using FastEndpoints;

namespace Aquifer.Public.API.Endpoints.Resources.Get.AvailableLanguages;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}/available-languages");
        Description(d => d.ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "For a given resource id, see in what other languages it is available.";
            s.Description = "For a given resource id, return a list of all other languages that are available for this resource.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Task.Delay(1, ct);
    }
}