using Aquifer.API.Common;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.Resources.ResourceContent;

public static class ResourceContentStatusEndpoints
{
    public static Ok<List<ResourceContentStatusDto>> GetList()
    {
        var statuses = Enum.GetValues(typeof(ResourceContentStatus))
            .Cast<ResourceContentStatus>()
            .Where(s => s != ResourceContentStatus.None)
            .Select(x => new ResourceContentStatusDto
            {
                DisplayName = x.GetDisplayName(),
                Status = x
            }).ToList();

        return TypedResults.Ok(statuses);
    }
}
