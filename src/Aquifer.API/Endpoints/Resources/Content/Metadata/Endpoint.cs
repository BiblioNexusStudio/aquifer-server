using Aquifer.API.Helpers;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Metadata;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    private const string AssociatedResourceQuery = """
                                                      SELECT
                                                          r.ExternalId,
                                                          r.Id AS ResourceId,
                                                          rc.Id AS ContentId
                                                      FROM
                                                          AssociatedResources ar
                                                          INNER JOIN Resources r ON r.Id = ar.AssociatedResourceId
                                                          INNER JOIN ResourceContents rc ON rc.ResourceId = ar.AssociatedResourceId AND rc.LanguageId = {0}
                                                          INNER JOIN ResourceContentVersions rcv ON rcv.ResourceContentId = rc.Id AND rcv.IsPublished = 1
                                                      WHERE
                                                          ar.ResourceId = {1}
                                                   """;

    public override void Configure()
    {
        Get("/resources/{ContentId}/metadata");
        Options(EndpointHelpers.SetCacheOption(5));
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var contentVersion = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == request.ContentId && rcv.IsPublished)
            .Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (contentVersion == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var associatedResources = await dbContext.Database
            .SqlQueryRaw<AssociatedResourceResponse>(AssociatedResourceQuery, contentVersion.ResourceContent.LanguageId,
                contentVersion.ResourceContent.ResourceId)
            .ToListAsync(ct);

        var response = new Response
        {
            Id = contentVersion.Id,
            DisplayName = contentVersion.DisplayName,
            Metadata = contentVersion.ResourceContent.MediaType == ResourceContentMediaType.Text
                ? null
                : JsonUtilities.DefaultDeserialize(contentVersion.Content),
            AssociatedResources = associatedResources
        };

        await SendOkAsync(response, ct);
    }
}