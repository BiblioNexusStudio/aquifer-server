using Aquifer.API.Common;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.Resources.ResourceContents;

public static class ResourceContentsStatusEndpoints
{
    public static Ok<List<ResourceContentsStatusResponse>> GetList()
    {
        var statuses = Enum.GetValues(typeof(ResourceContentStatus))
            .Cast<ResourceContentStatus>()
            .Where(s => s != ResourceContentStatus.None)
            .Select(x => new ResourceContentsStatusResponse
            {
                DisplayName = x.GetDisplayName(),
                Status = x
            }).ToList();

        return TypedResults.Ok(statuses);
    }
}