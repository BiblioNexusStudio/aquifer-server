using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Resources.ResourceContentItem;

public static class ResourceContentItemEndpoints
{
    public static async Task<Results<Ok<object>, NotFound, RedirectHttpResult>> GetResourceContentById(
        int contentId,
        AquiferDbContext dbContext,
        TelemetryClient telemetry,
        CancellationToken cancellationToken,
        [FromQuery] string audioType = "webm"
    )
    {
        var contentVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsPublished)
            .Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (contentVersion == null)
        {
            return TypedResults.NotFound();
        }

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Text)
        {
            return TypedResults.Ok(JsonUtilities.DefaultDeserialize(contentVersion.Content));
        }

        string? url = null;

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Audio)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentAudioJsonSchema>(contentVersion.Content);
            if (audioType == "webm" && deserialized.Webm != null)
            {
                url = deserialized.Webm.Url;
            }
            else if (deserialized.Mp3 != null)
            {
                url = deserialized.Mp3.Url;
            }
        }
        else
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentUrlJsonSchema>(contentVersion.Content);
            url = deserialized.Url;
        }

        if (url != null)
        {
            return TypedResults.Redirect(url);
        }

        telemetry.TrackTrace($"Content with ID {contentId} exists but has unexpected `Content` JSON.",
            SeverityLevel.Error);
        return TypedResults.NotFound();
    }

    public static async Task<Results<RedirectHttpResult, NotFound>> GetResourceThumbnailById(
        int contentId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        var contentVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsPublished)
            .Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (contentVersion == null)
        {
            return TypedResults.NotFound();
        }

        if (contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Video)
        {
            var deserialized = JsonUtilities.DefaultDeserialize<ResourceContentVideoJsonSchema>(contentVersion.Content);
            if (deserialized.ThumbnailUrl != null)
            {
                return TypedResults.Redirect(deserialized.ThumbnailUrl);
            }
        }

        return TypedResults.NotFound();
    }
}