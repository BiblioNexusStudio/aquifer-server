using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources;

public static class AdminResourcesHelpers
{
    public const string InvalidUserIdResponse = "The AssignedUserId was not valid.";

    public const string NoResourceFoundForContentIdResponse =
        "There is no ResourceContentVersion with the given contentId.";

    public const string DraftAlreadyExistsResponse = "This resource is already being aquiferized.";
    public const string NoDraftExistsResponse = "No draft currently exists for this resource.";
    public const string NotInReviewPendingResponse = "This resource is not in review pending status";

    public static async
        Task<(ResourceContentVersionEntity? latestVersion, ResourceContentVersionEntity? publishedVersion,
            ResourceContentVersionEntity? draftVersion)> GetResourceContentVersions(
            int contentId,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken)
    {
        var resourceContentVersions = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == contentId).Include(x => x.ResourceContent).ToListAsync(cancellationToken);

        return (
            resourceContentVersions.MaxBy(x => x.Version),
            resourceContentVersions.SingleOrDefault(x => x.IsPublished),
            resourceContentVersions.SingleOrDefault(x => x.IsDraft));
    }
}