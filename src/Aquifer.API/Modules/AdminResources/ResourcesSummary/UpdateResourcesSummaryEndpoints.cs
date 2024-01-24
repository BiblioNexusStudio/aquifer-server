using Aquifer.API.Modules.AdminResources.ResourceContentSummary;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Aquifer.API.Modules.AdminResources.ResourcesSummary;

public class UpdateResourcesSummaryEndpoints
{
    public static async Task<Results<NoContent, BadRequest<string>, NotFound>> UpdateResourceContentSummaryItem(
        int resourceContentId,
        ResourceContentSummaryItemUpdate item,
        AquiferDbContext dbContext,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.ResourceContentVersions
            .Where(x => x.IsDraft)
            .Where(x => x.ResourceContentId == resourceContentId)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);

        if (user.Id != entity.AssignedUserId)
        {
            return TypedResults.BadRequest("Not allowed to edit this resource");
        }

        if (item.Content is not null)
        {
            entity.Content = JsonUtilities.DefaultSerialize(item.Content);
        }
        if (item.WordCount is not null)
        {
            entity.WordCount = item.WordCount;
        }
        entity.DisplayName = item.DisplayName;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        entity.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}