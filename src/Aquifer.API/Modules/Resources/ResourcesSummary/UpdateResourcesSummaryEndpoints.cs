﻿using Aquifer.API.Utilities;
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
        var entity = await dbContext.ResourceContents.Where(x => x.Id == contentId)
            .Include(x => x.Resource).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        entity.Content = JsonUtilities.DefaultSerialize(item.Content);
        entity.Resource.EnglishLabel = item.Label;
        entity.Status = item.Status;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        entity.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}