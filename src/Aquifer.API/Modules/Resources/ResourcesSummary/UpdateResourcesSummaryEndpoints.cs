using Aquifer.API.Common;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace Aquifer.API.Modules.Resources.ResourceContentSummary;

public class UpdateResourcesSummaryEndpoints
{
    public static async Task<Results<NoContent, BadRequest<string>, NotFound>> UpdateResourceContentSummaryItem(int resourceContentId,
        ResourceContentSummaryItemUpdate item,
        AquiferDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.ResourceContentVersions
            .Where(x => x.IsDraft)
            .Where(x => x.ResourceContentId == resourceContentId)
            .Include(x => x.ResourceContent.Resource)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        string providerId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.ProviderId == providerId, cancellationToken) ??
            throw new ArgumentNullException();

        if (user.Id != entity.AssignedUserId)
        {
            return TypedResults.BadRequest("Not allowed to edit this resource");
        }

        entity.Content = JsonUtilities.DefaultSerialize(item.Content);
        entity.DisplayName = item.DisplayName;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        entity.ResourceContent.Updated = DateTime.UtcNow;
        entity.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}