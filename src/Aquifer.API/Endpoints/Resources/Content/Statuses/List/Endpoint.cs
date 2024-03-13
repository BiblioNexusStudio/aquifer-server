using Aquifer.Common.Extensions;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Resources.Content.Statuses.List;

public class Endpoint : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/admin/resources/content/statuses", "/resources/content/statuses");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var statuses = Enum.GetValues(typeof(ResourceContentStatus))
            .Cast<ResourceContentStatus>()
            .Where(s => s != ResourceContentStatus.None)
            .Select(x => new Response { DisplayName = x.GetDisplayName(), Status = x }).ToList();

        await SendOkAsync(statuses, ct);
    }
}