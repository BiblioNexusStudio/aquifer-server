using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Batch.Metadata;

public class Endpoint(AquiferDbContext dbContext, TelemetryClient telemetry) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/batch/metadata");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentVersions = await dbContext.ResourceContentVersions
            .Where(rcv => request.Ids.Contains(rcv.ResourceContentId) && rcv.IsPublished)
            .Select(rcv => new IntermediateContentVersion
            {
                Id = rcv.ResourceContentId,
                ResourceId = rcv.ResourceContent.ResourceId,
                DisplayName = rcv.DisplayName,
                MediaType = rcv.ResourceContent.MediaType,
                Content = rcv.Content,
                LanguageId = rcv.ResourceContent.LanguageId
            })
            .ToListAsync(ct);

        if (contentVersions.Count != request.Ids.Length)
        {
            telemetry.TrackTrace("IDs and metadata found have different lengths.",
                SeverityLevel.Error,
                new Dictionary<string, string> { { "Ids", string.Join(", ", request.Ids) } });
            await SendNotFoundAsync(ct);
            return;
        }

        var allLanguageIds = string.Join(',', contentVersions.Select(cv => cv.LanguageId).Distinct());
        var allResourceIds = string.Join(',', contentVersions.Select(cv => cv.ResourceId).Distinct());

        var associatedResourceQuery = $"""
                                           SELECT
                                               r.ExternalId,
                                               ar.ResourceId AS AssociatedToResourceId,
                                               r.Id AS ResourceId,
                                               rc.Id AS ContentId,
                                               rc.LanguageId
                                           FROM
                                               AssociatedResources ar
                                               INNER JOIN Resources r ON r.Id = ar.AssociatedResourceId
                                               INNER JOIN ResourceContents rc ON rc.ResourceId = ar.AssociatedResourceId AND rc.LanguageId IN ({allLanguageIds})
                                               INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = rc.Id AND rcv.IsPublished = 1
                                           WHERE
                                               ar.ResourceId IN ({allResourceIds})
                                       """;

        var associatedResources = await dbContext.Database
            .SqlQueryRaw<AssociatedResourceResponseWithLanguageAndAssociatedTo>(associatedResourceQuery)
            .ToListAsync(ct);

        var metadata = contentVersions.Select(contentVersion => new Response
        {
            Id = contentVersion.Id,
            DisplayName = contentVersion.DisplayName,
            Metadata = contentVersion.MediaType == ResourceContentMediaType.Text
                ? null
                : JsonUtilities.DefaultDeserialize(contentVersion.Content),
            AssociatedResources = associatedResources
                .Where(r => r.LanguageId == contentVersion.LanguageId && contentVersion.ResourceId == r.AssociatedToResourceId)
                .Select(r => new AssociatedResourceResponse
                {
                    ExternalId = r.ExternalId,
                    ResourceId = r.ResourceId,
                    ContentId = r.ContentId
                })
        }).ToList();

        await SendOkAsync(metadata, ct);
    }
}

public record AssociatedResourceResponseWithLanguageAndAssociatedTo : AssociatedResourceResponse
{
    public int AssociatedToResourceId { get; set; }
    public int LanguageId { get; set; }
}

public record IntermediateContentVersion
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public string DisplayName { get; set; } = null!;
    public ResourceContentMediaType MediaType { get; set; }
    public string Content { get; set; } = null!;
    public int LanguageId { get; set; }
}