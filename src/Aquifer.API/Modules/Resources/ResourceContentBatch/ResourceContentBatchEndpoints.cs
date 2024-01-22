using Aquifer.API.Services;
using Aquifer.API.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceContentBatch;

public static class ResourceContentBatchEndpoints
{
    public static async
        Task<Results<Ok<List<ResourceItemMetadataWithIdResponse>>, BadRequest<string>, NotFound<string>>>
        GetResourceMetadataByIds(
            [FromQuery] int[] ids,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            TelemetryClient telemetry
        )
    {
        if (ids.Length > 100)
        {
            return TypedResults.BadRequest("A maximum of 100 ids may be passed.");
        }

        var metadata = await dbContext.ResourceContentVersions
            .Where(rcv => ids.Contains(rcv.ResourceContentId) && rcv.IsPublished)
            .Select(contentVersion => new ResourceItemMetadataWithIdResponse
            {
                Id = contentVersion.ResourceContentId,
                DisplayName = contentVersion.DisplayName,
                Metadata = contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Text
                    ? null
                    : JsonUtilities.DefaultDeserialize(contentVersion.Content)
            })
            .ToListAsync(cancellationToken);

        if (metadata.Count != ids.Length)
        {
            telemetry.TrackTrace("IDs and metadata found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", ids) } });
            return TypedResults.NotFound("One or more couldn't be found");
        }

        return TypedResults.Ok(metadata);
    }

    public static async Task<Results<Ok<List<ResourceItemTextContentResponse>>, BadRequest<string>, NotFound<string>>>
        GetResourceTextContentByIds(
            [FromQuery] int[] ids,
            AquiferDbContext dbContext,
            CancellationToken cancellationToken,
            TelemetryClient telemetry
        )
    {
        if (ids.Length > 10)
        {
            return TypedResults.BadRequest("A maximum of 10 ids may be passed.");
        }

        var contents = await dbContext.ResourceContentVersions
            .Where(rcv =>
                ids.Contains(rcv.ResourceContentId) &&
                rcv.IsPublished &&
                rcv.ResourceContent.MediaType == ResourceContentMediaType.Text)
            .Select(contentVersion => new ResourceItemTextContentResponse
            {
                Id = contentVersion.ResourceContentId,
                Content = JsonUtilities.DefaultDeserialize(contentVersion.Content)
            })
            .ToListAsync(cancellationToken);

        if (contents.Count != ids.Length)
        {
            telemetry.TrackTrace("IDs and content found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", ids) } });
            return TypedResults.NotFound("One or more couldn't be found. Were some IDs for non-text content?");
        }

        return TypedResults.Ok(contents);
    }
}