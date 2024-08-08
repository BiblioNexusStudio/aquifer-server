﻿using Aquifer.Common.Services;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public class Endpoint(AquiferDbContext dbContext, IResourceContentRequestTrackingService trackingService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}");
        Description(d => d.ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "Get specific resource information.";
            s.Description =
                "For a given resource id, return the data for that resource. This can be text content as well as CDN links for image, audio, and video media types.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await GetResourceContentAsync(req, ct);
        await SendOkAsync(response, ct);
    }

    private async Task<Response> GetResourceContentAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.ResourceContentVersions
            .Where(x => x.ResourceContentId == req.ContentId && x.IsPublished && x.ResourceContent.Resource.ParentResource.Enabled)
            .Select(x => new Response
            {
                Id = x.ResourceContentId,
                Name = x.ResourceContent.Resource.EnglishLabel,
                LocalizedName = x.DisplayName,
                ContentValue = x.Content,
                Language = new ResourceContentLanguage
                {
                    Id = x.ResourceContent.Language.Id,
                    DisplayName = x.ResourceContent.Language.EnglishDisplay,
                    Code = x.ResourceContent.Language.ISO6393Code,
                    ScriptDirection = x.ResourceContent.Language.ScriptDirection
                },
                Grouping = new ResourceTypeMetadata
                {
                    Name = x.ResourceContent.Resource.ParentResource.DisplayName,
                    Type = x.ResourceContent.Resource.ParentResource.ResourceType,
                    MediaTypeValue = x.ResourceContent.MediaType,
                    LicenseInfoValue = x.ResourceContent.Resource.ParentResource.LicenseInfo
                }
            })
            .SingleOrDefaultAsync(ct);

        if (response is null)
        {
            ThrowError($"No record found for {req.ContentId}", 404);
        }

        response.Content = TiptapUtilities.ConvertFromJson(response.ContentValue,
            response.Grouping.MediaTypeValue == ResourceContentMediaType.Text ? req.ContentTextType : TiptapContentType.None);
        return response;
    }

    public override async Task OnAfterHandleAsync(Request req, Response res, CancellationToken ct)
    {
        const string endpointId = "public-resources-get";
        await trackingService.TrackAsync(HttpContext, req.ContentId, endpointId, "public-api");
    }
}