using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceContentItem;

public static class ResourceContentItemEndpoints
{
    public static async Task<Results<RedirectHttpResult, NotFound>> GetResourceThumbnailById(int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var contentVersion = await dbContext.ResourceContentVersions.Where(rcv => rcv.ResourceContentId == contentId && rcv.IsPublished)
            .Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (contentVersion == null)
        {
            return TypedResults.NotFound();
        }

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Video)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentVideoJsonSchema>(contentVersion.Content);
            if (deserialized.ThumbnailUrl != null)
            {
                return TypedResults.Redirect(deserialized.ThumbnailUrl);
            }
        }

        return TypedResults.NotFound();
    }
}