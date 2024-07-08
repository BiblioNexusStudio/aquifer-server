using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Search;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/search");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var resources = await dbContext.ResourceContentVersions.Where(x =>
                x.IsPublished &&
                x.DisplayName.Contains(request.Query) &&
                x.ResourceContent.Resource.ParentResource.Enabled &&
                request.ResourceTypes.Contains(x.ResourceContent.Resource.ParentResource.ResourceType) &&
                x.ResourceContent.LanguageId == request.LanguageId).Select(rcv => new Response
                {
                    Id = rcv.ResourceContentId,
                    DisplayName = rcv.DisplayName,
                    MediaType = rcv.ResourceContent.MediaType.ToString(),
                    ParentResourceId = rcv.ResourceContent.Resource.ParentResourceId,
                    Version = rcv.Version,
                    ResourceType = rcv.ResourceContent.Resource.ParentResource.ResourceType.ToString()
                })
            .OrderBy(r => r.DisplayName)
            .ToListAsync(ct);

        await SendOkAsync(resources, ct);
    }
}