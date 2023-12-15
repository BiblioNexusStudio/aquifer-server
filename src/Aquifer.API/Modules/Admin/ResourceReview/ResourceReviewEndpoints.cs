using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aquifer.API.Modules.Admin.ResourceReview;

public static class ResourceReviewEndpoints
{
    
    public static async Task<Results<Ok<string>, BadRequest<string>>> Review(int contentId,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        (var mostRecentResourceContentVersion, string badRequestResponse) =
            await GetResourceContentVersionValidation(contentId,
                dbContext,
                cancellationToken);

        if (mostRecentResourceContentVersion is null)
        {
            return TypedResults.BadRequest(badRequestResponse);
        }
        // get current user
        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
                       throw new ArgumentNullException();

        mostRecentResourceContentVersion.Updated = DateTime.UtcNow;
        mostRecentResourceContentVersion.AssignedUserId = user.Id;
        
        var resourceContent =
            await dbContext.ResourceContents.FirstOrDefaultAsync(x => x.Id == contentId, cancellationToken) ??
            throw new ArgumentNullException();
        resourceContent.Status = ResourceContentStatus.AquiferizeInReview;

        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = mostRecentResourceContentVersion.Id,
            Status = ResourceContentStatus.AquiferizeInReview,
            ChangedByUserId = user.Id,
            Created = DateTime.UtcNow
        };
        dbContext.ResourceContentVersionStatusHistory.Add(resourceContentVersionStatusHistory);
        await dbContext.SaveChangesAsync(cancellationToken);

        // return a 200
        return TypedResults.Ok("Success");
    }

    private static async Task<(ResourceContentVersionEntity?, string)> GetResourceContentVersionValidation(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId).ToListAsync(cancellationToken);

        // First check that a ResourceContentVersion exists with the given contentId and IsDraft = 1 and ResourceContent.Status is AquiferizationReviewPending. If none exists, return a 400.
        bool hasResourceContentDraft = resourceContentVersions
                                           .Any(x => x.ResourceContentId == contentId && 
                                           x.IsDraft && 
                                           x.ResourceContent.Status == ResourceContentStatus.AquiferizeReviewPending );

        if (!hasResourceContentDraft)
        {
            return (null, "This resource either does not have a draft that has the status of Aquaferization Review pending");
        }

        // Grab the most recent ResourceContentVersion with given contentId and IsDraft = 1
        var mostRecentResourceContentVersion = resourceContentVersions
            .Where(x => x.ResourceContentId == contentId && x.IsDraft).MaxBy(x => x.Version);

        if (mostRecentResourceContentVersion is null)
        {
            return (null, "There is no draft ResourceContentVersion with the given contentId.");
        }

        return (mostRecentResourceContentVersion, string.Empty);
    }
}