using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Security.Claims;

namespace Aquifer.API.Modules.Admin.ResourceReview;

public static class ResourceReviewEndpoints
{
    
    public static async Task<Results<Ok<string>, BadRequest<string>>> Review(int contentId,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        (var mostRecentResourceContentVersion, var resourceContent, string badRequestResponse) =
            await GetResourceContentVersionValidation(contentId,
                dbContext,
                cancellationToken);

        if (mostRecentResourceContentVersion is null || resourceContent is null)
        {
            return TypedResults.BadRequest(badRequestResponse);
        }
        // get current user
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                       throw new ArgumentNullException();

        mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
        mostRecentResourceContentVersion.AssignedUserId = user.Id;
        
        resourceContent.Status = ResourceContentStatus.AquiferizeInReview;

        // update version status history
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = mostRecentResourceContentVersion.Id,
            Status = ResourceContentStatus.AquiferizeInReview,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);

        // update version assigned user history
        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
            {
                ResourceContentVersion = mostRecentResourceContentVersion,
                AssignedUserId = user.Id,
                ChangedByUserId = user.Id,
                Created = DateTime.UtcNow
            };
        dbContext.ResourceContentVersionAssignedUserHistory.Add(resourceContentVersionAssignedUserHistory);
        await dbContext.SaveChangesAsync(cancellationToken);

        // return a 200
        return TypedResults.Ok("Success");
    }

    private static async Task<(ResourceContentVersionEntity?, ResourceContentEntity?, string)> GetResourceContentVersionValidation(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft == true).ToListAsync(cancellationToken);

        // First check that a ResourceContentVersion exists with the given contentId and IsDraft = 1
        if (resourceContentVersions.Count == 0)
        {
            return (null, null, "This resource does not have a draft");
        }

        var resourceContent =
            await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken) ??
            throw new ArgumentNullException();

        if (resourceContent.Status != ResourceContentStatus.AquiferizeReviewPending) {
            return (null, null, "This resource is not in AquiferizeReviewPending status");
        }

        // Grab the most recent ResourceContentVersion with given contentId and IsDraft = 1
        var mostRecentResourceContentVersion = resourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft).MaxBy(x => x.Version);

        if (mostRecentResourceContentVersion is null)
        {
            return (null, null, "Draft with the given contentId does not exist.");
        }
        return (mostRecentResourceContentVersion, resourceContent, string.Empty);
    }
}