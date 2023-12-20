using Aquifer.API.Common;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.AdminResources.ResourceContent;

public static class ResourceContentStatusEndpoints
{
    public static Ok<List<ResourceContentStatusResponse>> GetList()
    {
        var statuses = Enum.GetValues(typeof(ResourceContentStatus))
            .Cast<ResourceContentStatus>()
            .Where(s => s != ResourceContentStatus.None)
            .Select(x => new ResourceContentStatusResponse
            {
                DisplayName = x.GetDisplayName(),
                Status = x
            }).ToList();

        return TypedResults.Ok(statuses);
    }
}