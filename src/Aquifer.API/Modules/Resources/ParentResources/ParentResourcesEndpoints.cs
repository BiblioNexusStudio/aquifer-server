using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ParentResources;

public static class ParentResourcesEndpoints
{
    public static async Task<Ok<List<ParentResourceDto>>> Get(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var parentResources = await dbContext.ParentResources
            .Select(x => new ParentResourceDto
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
