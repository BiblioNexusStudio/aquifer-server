using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.NotApplicable.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/not-applicable");
        Permissions(PermissionName.SetStatusCompleteNotApplicable);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response = await dbContext.ResourceContents
            .Where(x => x.Status == ResourceContentStatus.TranslationNotApplicable)
            .Select(
                x => new Response
                {
                    Id = x.Id,
                    Title = x.Resource.EnglishLabel,
                    NotApplicableReason = x.NotApplicableReason,
                    ParentResourceName = x.Resource.ParentResource.DisplayName,
                    Language = x.Language.EnglishDisplay,
                    ProjectName = x.ProjectResourceContents.SingleOrDefault() == null
                        ? null
                        : x.ProjectResourceContents.Single().Project.Name
                })
            .ToListAsync(ct);
    }
}