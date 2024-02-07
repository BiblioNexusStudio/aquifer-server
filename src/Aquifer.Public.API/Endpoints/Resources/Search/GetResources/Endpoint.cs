﻿using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Public.API.Helpers;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Search.GetResources;

public class Endpoint(AquiferDbContext _dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/search");
        Options(EndpointHelpers.SetCacheOption());
        Summary(s =>
        {
            s.Summary = "Search resources by keyword query, passage, or both.";
            s.Description =
                "For a given query, language, and content type, search for matching resources.";
        });
    }

    public override void OnValidationFailed()
    {
        var resourceTypeFailure = ValidationFailures.FirstOrDefault(x => x.PropertyName.ToLower() == "resourcetype");
        if (resourceTypeFailure is not null)
        {
            var validValues = string.Join(", ", Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>().ToList());
            var errorMessage = $"{resourceTypeFailure.ErrorMessage} Valid values are {validValues}";
            resourceTypeFailure.ErrorMessage = errorMessage;
        }

        base.OnValidationFailed();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = GetQuery(req);
        var totalCount = await GetTotalResourceCountAsync(req, query, ct);

        if (totalCount == 0)
        {
            await SendAsync(new Response(), 200, ct);
            return;
        }

        var items = await GetResourcesAsync(req, query, ct);
        var response = new Response
        {
            TotalItemCount = totalCount,
            ReturnedItemCount = items.Count,
            Items = items
        };

        await SendAsync(response, 200, ct);
    }

    private IQueryable<ResourceContentVersionEntity> GetQuery(Request req)
    {
        var (startVerseId, endVerseId) = req.BookCode is null
            ? ((int?)null, (int?)null)
            : BibleUtilities.GetVerseIds(req.BookCode, req.StartChapter, req.EndChapter, req.StartVerse, req.EndVerse);

        return _dbContext.ResourceContentVersions.Where(x =>
            x.IsPublished &&
            (req.Query == null || x.DisplayName.Contains(req.Query) || x.ResourceContent.Resource.EnglishLabel.Contains(req.Query)) &&
            (startVerseId == null ||
                x.ResourceContent.Resource.VerseResources.Any(vr =>
                    vr.VerseId >= startVerseId && vr.VerseId <= endVerseId) ||
                x.ResourceContent.Resource.PassageResources.Any(pr =>
                    (pr.Passage.StartVerseId >= startVerseId && pr.Passage.StartVerseId <= endVerseId) ||
                    (pr.Passage.EndVerseId >= startVerseId && pr.Passage.EndVerseId <= endVerseId) ||
                    (pr.Passage.StartVerseId <= startVerseId && pr.Passage.EndVerseId >= endVerseId))) &&
            (req.ResourceType == default || x.ResourceContent.Resource.ParentResource.ResourceType == req.ResourceType) &&
            (x.ResourceContent.LanguageId == req.LanguageId || x.ResourceContent.Language.ISO6393Code == req.LanguageCode));
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

    private async Task<List<ResponseContent>> GetResourcesAsync(Request req,
        IQueryable<ResourceContentVersionEntity> query,
        CancellationToken ct)
    {
        var resources = await query.OrderBy(x => x.ResourceContent.Resource.EnglishLabel)
            .Skip(req.Offset)
            .Take(req.Limit)
            .Select(x => new ResponseContent
            {
                Id = x.ResourceContent.Id,
                Name = x.ResourceContent.Resource.EnglishLabel,
                LocalizedName = x.DisplayName,
                LanguageCode = x.ResourceContent.Language.ISO6393Code,
                MediaType = x.ResourceContent.MediaType,
                Grouping = new ResourceTypeMetadata
                {
                    Name = x.ResourceContent.Resource.ParentResource.DisplayName,
                    Type = x.ResourceContent.Resource.ParentResource.ResourceType
                }
            })
            .ToListAsync(ct);

        return resources;
    }
}