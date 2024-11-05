using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Jobs.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.Start;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    IResourceHistoryService resourceHistoryService,
    ITranslationPublisher translationPublisher)
    : Endpoint<Request>
{
    public override void Configure()
    {
        Post("/projects/{Id}/start");
        Permissions(PermissionName.EditProject);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var project = await dbContext.Projects
            .AsTracking()
            .Include(p => p.ProjectPlatform)
            .SingleOrDefaultAsync(p => p.Id == request.Id, ct);

        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        ValidateProject(project);

        var resourceContentVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv => rcv.ResourceContent.ProjectResourceContents.Any(prc => prc.Project.Id == project.Id) && rcv.IsDraft)
            .ToListAsync(ct);

        // create original language snapshots before queuing for translation
        foreach (var resourceContentVersion in resourceContentVersions)
        {
            await resourceHistoryService.AddSnapshotHistoryAsync(resourceContentVersion, user.Id, ResourceContentStatus.New, ct);
        }

        project.Started = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);

        await translationPublisher.PublishTranslateProjectResourcesMessageAsync(
            new TranslateProjectResourcesMessage(project.Id, user.Id),
            ct);

        await SendNoContentAsync(ct);
    }

    private void ValidateProject(ProjectEntity project)
    {
        if (project.Started is not null)
        {
            AddError("Project is already started.");
        }

        if (project.QuotedCost is null)
        {
            AddError("Quoted Cost must be set before starting.");
        }

        if (project.ProjectedDeliveryDate is null)
        {
            AddError("Projected Delivery Date must be set before starting.");
        }

        if (project.ProjectedPublishDate is null)
        {
            AddError("Projected Publish Date must be set before starting.");
        }

        if (project.EffectiveWordCount is null)
        {
            AddError("Effective Word Count must be set before starting.");
        }

        if (project.ProjectPlatform.Name == "Aquifer" && project.CompanyLeadUserId is null)
        {
            AddError("Company Lead User must be set before starting.");
        }

        ThrowIfAnyErrors();
    }
}