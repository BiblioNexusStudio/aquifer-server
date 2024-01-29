using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Reports.EditedResources;

/// <summary>
///     Handles the retrieval of edited resources
/// </summary>
public class EditedResourcesLastThirtyDays
{
    /// <summary>
    ///     Handles the retrieval of resources edited in the last thirty days.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP result.</returns>
    public static async Task<IResult> HandleAsync(
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var lastThirtyDays = DateTime.UtcNow.AddDays(-30);
        var editedResources = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.Created >= lastThirtyDays)
            .Select(rvc => new
            {
                rvc.Version,
                rvc.IsDraft,
                rvc.DisplayName,
                rvc.WordCount
            })
            .ToListAsync(cancellationToken);

        return Results.Ok(editedResources);
    }
}