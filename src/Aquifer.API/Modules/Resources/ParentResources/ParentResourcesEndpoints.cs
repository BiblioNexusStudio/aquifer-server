using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ParentResources;

public static class ParentResourcesEndpoints
{
    public static async Task<Ok<List<ParentResourceResponse>>> Get(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var parentResources = await dbContext.ParentResources
            .Select(x => new ParentResourceResponse
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                SerializedLicenseInfo = x.LicenseInfo,
                ComplexityLevel = x.ComplexityLevel
            })
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(parentResources);
    }
}