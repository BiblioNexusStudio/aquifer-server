using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using static Aquifer.API.Helpers.EndpointHelpers;

namespace Aquifer.API.Endpoints.Projects.Update;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request>
{
    public override void Configure()
    {
        Put("/projects/{Id}");
        Permissions(PermissionName.EditProject);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var project = await dbContext.Projects.Include(p => p.ProjectPlatform).SingleOrDefaultAsync(p => p.Id == request.Id, ct);
        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await ValidateRequest(project, request, ct);
        UpdateProject(request, project);

        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }

    private static void UpdateProject(Request request, ProjectEntity project)
    {
        if (request.QuotedCost.HasValue)
        {
            project.QuotedCost = request.QuotedCost;
        }

        if (request.EffectiveWordCount.HasValue)
        {
            project.EffectiveWordCount = request.EffectiveWordCount;
        }

        if (request.ProjectedDeliveryDate.HasValue)
        {
            project.ProjectedDeliveryDate = request.ProjectedDeliveryDate;
        }

        if (request.ProjectedPublishDate.HasValue)
        {
            project.ProjectedPublishDate = request.ProjectedPublishDate;
        }

        if (request.ProjectManagerUserId.HasValue)
        {
            project.ProjectManagerUserId = request.ProjectManagerUserId.Value;
        }

        if (request.CompanyLeadUserId.HasValue)
        {
            project.CompanyLeadUserId = request.CompanyLeadUserId;
        }
    }

    private async Task ValidateRequest(ProjectEntity project, Request request, CancellationToken ct)
    {
        if (project.Started is not null)
        {
            ThrowError(r => r.Id, "Can't make updates to a project that is already started.");
        }

        if (project.ProjectPlatform.Name != "Aquifer" && request.CompanyLeadUserId is not null)
        {
            ThrowError(r => r.Id, "Unable to set a company lead user for a non-Aquifer platform.");
        }

        if (request.CompanyLeadUserId is not null &&
            await dbContext.Users.SingleOrDefaultAsync(p => p.Id == request.CompanyLeadUserId, ct) is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.CompanyLeadUserId);
        }

        if (request.ProjectManagerUserId is not null)
        {
            var projectManagerUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.ProjectManagerUserId, ct);
            if (projectManagerUser is null)
            {
                ThrowEntityNotFoundError<Request>(r => r.ProjectManagerUserId);
            }

            if (projectManagerUser.Role != UserRole.Publisher)
            {
                ThrowError(r => r.ProjectManagerUserId, "Project manager must be a Publisher.");
            }
        }
    }
}