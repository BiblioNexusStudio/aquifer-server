using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Aquifer.API.Modules.Resources.ResourceContents;

public static class ResourceContentsStatusEndpoints
{
    public static Ok<List<ResourceContentsStatusResponse>> GetList(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var statuses = new List<ResourceContentsStatusResponse>();
        foreach (var value in Enum.GetValues(typeof(ResourceContentStatus)))
        {
            var status = (ResourceContentStatus)value;
            string? displayName = null;

            switch (status)
            {
                case ResourceContentStatus.None:
                    break;
                case ResourceContentStatus.AquiferizeNotStarted:
                    displayName = "Aquiferize - Not Started";
                    break;
                case ResourceContentStatus.AquiferizeInProgress:
                    displayName = "Aquiferize - In Progress";
                    break;
                case ResourceContentStatus.Complete:
                    displayName = "Complete";
                    break;
                case ResourceContentStatus.AquiferizeInReview:
                    displayName = "Aquiferize - In Review";
                    break;
                case ResourceContentStatus.TranslateNotStarted:
                    displayName = "Translate - Not Started";
                    break;
                case ResourceContentStatus.TranslateDrafting:
                    displayName = "Translate - Drafting";
                    break;
                case ResourceContentStatus.TranslateEditing:
                    displayName = "Translate - Editing";
                    break;
                case ResourceContentStatus.TranslateReviewing:
                    displayName = "Translate - Reviewing";
                    break;
                case ResourceContentStatus.OnHold:
                    displayName = "On Hold";
                    break;
            }
            if (displayName != null)
            {
                statuses.Add(new ResourceContentsStatusResponse
                {
                    Status = status,
                    DisplayName = displayName
                });
            }
        }

        return TypedResults.Ok(statuses);
    }
}