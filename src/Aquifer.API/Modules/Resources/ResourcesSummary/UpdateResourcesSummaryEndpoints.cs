using Aquifer.API.Utilities;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace Aquifer.API.Modules.Resources.ResourcesSummary;

public class UpdateResourcesSummaryEndpoints
{
    public static async Task<Results<NoContent, NotFound>> UpdateResourcesSummaryItem(int contentId,
        ResourcesSummaryItemUpdate item,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.ResourceContents.FindAsync(contentId, cancellationToken);
        if (entity == null)
        {
            return TypedResults.NotFound();
        }

        var content = JsonUtilities.DefaultDeserialize<List<ResourcesSummaryTiptapContent>>(entity.Content);
        content.Single(x => !(item.TiptapContent.StepNumber > 0) || x.StepNumber == item.TiptapContent.StepNumber)
            .Tiptap = item.TiptapContent.Tiptap;

        entity.Content = JsonUtilities.DefaultSerialize(content);
        entity.DisplayName = item.Label;
        entity.Status = item.Status;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        entity.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}