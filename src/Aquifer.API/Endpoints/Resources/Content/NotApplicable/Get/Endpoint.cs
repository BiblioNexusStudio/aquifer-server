using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable.Get;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<IEnumerable<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/not-applicable");
        Permissions(PermissionName.SetStatusCompleteNotApplicable);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var queryResults = await dbContext.ResourceContentVersions
            .Include(x => x.ResourceContent)
            .ThenInclude(rc => rc.Resource)
            .ThenInclude(r => r.ParentResource)
            .Include(x => x.ResourceContent)
            .ThenInclude(rc => rc.ProjectResourceContents)
            .ThenInclude(prc => prc.Project)
            .Include(x => x.ResourceContent)
            .ThenInclude(rc => rc.Language)
            .Where(x => x.ResourceContent.Status == ResourceContentStatus.TranslationNotApplicable)
            .ToListAsync(ct);

        Response = queryResults.Select(rcv => new Response
        {
            Id = rcv.ResourceContentId,
            Title = rcv.DisplayName,
            ParentResourceName = rcv.ResourceContent.Resource.ParentResource.DisplayName,
            Language = rcv.ResourceContent.Language.EnglishDisplay,
            ProjectName = rcv.ResourceContent.ProjectResourceContents.FirstOrDefault() == null
                ? null
                : rcv.ResourceContent.ProjectResourceContents.First().Project.Name
        });
    }
}