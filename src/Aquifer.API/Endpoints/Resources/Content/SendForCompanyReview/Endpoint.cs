using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.SendForCompanyReview;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        // TODO: remove *-manager-review paths when confident they are depricated fully. - Kasey
        Post(
            "/resources/content/{ContentId}/send-for-manager-review",
            "/resources/content/send-for-manager-review",
            "/resources/content/{ContentId}/send-for-company-review",
            "/resources/content/send-for-company-review"
        );
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentIds = request.ContentId is not null ? [request.ContentId.Value] : request.ContentIds!;
        List<ResourceContentStatus> allowedStatuses =
        [
            ResourceContentStatus.AquiferizeEditorReview, ResourceContentStatus.TranslationEditorReview
        ];

        var draftVersions = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(x => contentIds.Contains(x.ResourceContentId) && allowedStatuses.Contains(x.ResourceContent.Status) && x.IsDraft)
            .Include(x => x.ResourceContent)
            .ThenInclude(x => x.ProjectResourceContents)
            .ThenInclude(x => x.Project)
            .ToListAsync(ct);

        if (draftVersions.Count != contentIds.Count)
        {
            ThrowError("One or more resources not found or not in correct status");
        }

        var user = await userService.GetUserWithCompanyUsersFromJwtAsync(ct);
        var managerIds = user.Company.Users.Where(x => x.Role == UserRole.Manager && user.Enabled).Select(x => x.Id).ToList();
        foreach (var draftVersion in draftVersions)
        {
            if (user.Id != draftVersion.AssignedUserId)
            {
                ThrowError("User must be assigned to content to send for company review.");
            }

            await historyService.AddSnapshotHistoryAsync(draftVersion,
                draftVersion.AssignedUserId,
                draftVersion.ResourceContent.Status,
                ct);

            var reviewPendingStatus = draftVersion.ResourceContent.Status == ResourceContentStatus.TranslationEditorReview
                ? ResourceContentStatus.TranslationCompanyReview
                : ResourceContentStatus.AquiferizeCompanyReview;

            draftVersion.ResourceContent.Status = reviewPendingStatus;
            await SetAssignedUserId(user,
                draftVersion.ResourceContent.ProjectResourceContents.FirstOrDefault(x => x.Project.ActualPublishDate == null)?.Project,
                managerIds,
                draftVersion);

            await historyService.AddAssignedUserHistoryAsync(draftVersion, draftVersion.AssignedUserId, user.Id, ct);
            await historyService.AddStatusHistoryAsync(draftVersion, reviewPendingStatus, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);

        Response.Assignments = draftVersions.Select(x => new UserAssignment
            {
                ResourceContentId = x.ResourceContentId,
                AssignedUserId = x.AssignedUserId!.Value
            })
            .ToList();
    }

    private async Task SetAssignedUserId(UserEntity user,
        ProjectEntity? project,
        List<int> managers,
        ResourceContentVersionEntity draftVersion)
    {
        const string errorMessage = "Can't find manager to assign to";

        var companyReviewer = user.Company.CompanyReviewers.SingleOrDefault(x => x.LanguageId == draftVersion.ResourceContent.LanguageId);

        if (draftVersion.AssignedReviewerUserId is not null)
        {
            draftVersion.AssignedUserId = draftVersion.AssignedReviewerUserId;
        }
        else if (companyReviewer is not null)
        {
            draftVersion.AssignedUserId = companyReviewer.UserId;
        }
        else if (project?.CompanyLeadUserId is not null)
        {
            draftVersion.AssignedUserId = project.CompanyLeadUserId;
        }
        else if (user.Role is UserRole.Manager or UserRole.Publisher)
        {
            draftVersion.AssignedUserId = user.Id;
        }
        else
        {
            var lastAssignmentHistory = await dbContext.ResourceContentVersionAssignedUserHistory
                .AsTracking()
                .Where(x => x.ResourceContentVersionId == draftVersion.Id && managers.Contains(x.ChangedByUserId))
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (lastAssignmentHistory is not null)
            {
                draftVersion.AssignedUserId = lastAssignmentHistory.ChangedByUserId;
            }
            else if (managers.Count > 1)
            {
                draftVersion.AssignedUserId = managers[0];
            }
            else
            {
                ThrowError(x => x.ContentId, errorMessage);
            }
        }
    }
}