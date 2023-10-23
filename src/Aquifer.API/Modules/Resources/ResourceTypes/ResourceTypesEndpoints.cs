using Aquifer.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceTypes;

public static class ResourceTypesEndpoints
{
    public static async Task<Ok<List<ResourceTypeResponse>>> Get(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var resourceTypes = await dbContext.ResourceTypes
            .Select(x => new ResourceTypeResponse
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                SerializedLicenseInfo = x.LicenseInfo,
                ComplexityLevel = x.ComplexityLevel
            })
            .ToListAsync(cancellationToken);

        return TypedResults.Ok(resourceTypes);
    }
}