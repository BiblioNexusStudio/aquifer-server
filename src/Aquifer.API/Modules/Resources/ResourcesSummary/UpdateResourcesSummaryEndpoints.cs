using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public class UpdateResourcesSummaryEndpoints
{
    public static async Task<Results<NoContent, NotFound>> UpdateResourcesSummaryItem(int contentId,
        ResourcesSummaryItemUpdate item,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        // When needed, inject ClaimsPrincipal claimsPrincipal to get the user information
        // string userClaim = claimsPrincipal.Claims.Single(x => x.Type == "user").Value;

        var entity = await dbContext.ResourceContentVersions
            .Where(x => x.IsPublished) // TODO: swap this with IsDraft once we can create drafts
            .Where(x => x.ResourceContentId == contentId)
            .Include(x => x.ResourceContent.Resource)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        entity.Content = JsonUtilities.DefaultSerialize(item.Content);
        entity.ResourceContent.Resource.EnglishLabel = item.Label;
        entity.ResourceContent.Status = item.Status;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        entity.ResourceContent.Updated = DateTime.UtcNow;
        entity.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}