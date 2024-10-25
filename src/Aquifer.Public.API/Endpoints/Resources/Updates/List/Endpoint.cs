using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Endpoint(AquiferDbContext dbContext, ICachingLanguageService cachingLanguageService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/updates");
        Description(d => d
            .WithTags("Resources")
            .ProducesProblemFE());
        Summary(s =>
        {
            s.Summary = "Get resource ids that are new or updated since the given UTC timestamp.";
            s.Description =
                "For a given UTC timestamp, get a list of resource ids that are new or have been updated since the provided timestamp. This is intended for users who are storing Aquifer data locally and want to fetch new content.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = await GetQuery(req, ct);
        var totalCount = await GetTotalResourceCountAsync(req, query, ct);

        if (totalCount == 0)
        {
            await SendOkAsync(new Response
            {
                Offset = req.Offset
            }, ct);
            return;
        }

        var items = await GetResourceUpdatesAsync(req, query, ct);
        Response = new Response
        {
            Items = items,
            TotalItemCount = totalCount,
            Offset = req.Offset
        };
    }

    private async Task<List<ResponseContent>> GetResourceUpdatesAsync(Request req, IQueryable<ResourceContentVersionEntity> query,
        CancellationToken ct)
    {
        var languageCodeByIdMap = await cachingLanguageService.GetLanguageCodeByIdMapAsync(ct);
        return await query
            .OrderByDescending(r => r.Updated)
            .Skip(req.Offset)
            .Take(req.Limit)
            .Select(x => new ResponseContent
            {
                ResourceId = x.ResourceContentId,
                LanguageId = x.ResourceContent.LanguageId,
                LanguageCode = languageCodeByIdMap[x.ResourceContent.LanguageId],
                UpdateType = x.Version == 1 ? ResponseContentUpdateType.New : ResponseContentUpdateType.Updated,
                Timestamp = x.Updated
            })
            .ToListAsync(ct);
    }

    private async Task<int> GetTotalResourceCountAsync(Request req, IQueryable<ResourceContentVersionEntity> query, CancellationToken ct)
    {
        var totalCount = await query.CountAsync(ct);
        if (req.Offset >= totalCount && req.Offset != 0)
        {
            ThrowError($"Offset of {req.Offset} equals or exceeds total of {totalCount}");
        }

        return totalCount;
    }

    private async Task<IQueryable<ResourceContentVersionEntity>> GetQuery(Request req, CancellationToken ct)
    {
        var (isValidLanguageId, isValidLanguageCode, validLanguageId) =
            await cachingLanguageService.ValidateLanguageIdOrCodeAsync(req.LanguageId, req.LanguageCode, false, ct);

        if (!isValidLanguageCode)
        {
            ThrowError($"Invalid LanguageCode:  '{req.LanguageCode}'");
        }

        if (!isValidLanguageId)
        {
            ThrowError($"Invalid LanguageId: '{req.LanguageId}'");
        }

        return dbContext.ResourceContentVersions
            .Where(x => (!validLanguageId.HasValue || x.ResourceContent.LanguageId == validLanguageId) &&
                        x.IsPublished &&
                        x.Updated >= req.Timestamp &&
                        (req.ResourceCollectionCode == null ||
                         x.ResourceContent.Resource.ParentResource.Code.ToLower() == req.ResourceCollectionCode.ToLower()));
    }
}