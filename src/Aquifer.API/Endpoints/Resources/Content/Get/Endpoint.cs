using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/{Id}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var englishLanguageId = await dbContext.Languages.Where(l => l.ISO6393Code == "eng").Select(l => l.Id).SingleAsync(ct);
        var resourceContent = await dbContext.ResourceContents.Where(x => x.Id == request.Id)
            .Select(rc => new Response
            {
                EnglishLabel = rc.Resource.EnglishLabel,
                ParentResourceName = rc.Resource.ParentResource.DisplayName,
                ResourceContentId = rc.Id,
                ResourceId = rc.ResourceId,
                Language = new LanguageResponse { EnglishDisplay = rc.Language.EnglishDisplay, ISO6393Code = rc.Language.ISO6393Code },
                Status = rc.Status,
                MediaType = rc.MediaType,
                HasAudio = rc.Resource.ResourceContents.Any(orc =>
                    orc.LanguageId == rc.LanguageId &&
                    orc.MediaType == ResourceContentMediaType.Audio),
                ContentTranslations = rc.Resource.ResourceContents
                    .Where(orc => orc.MediaType == rc.MediaType)
                    .Select(orc => new TranslationResponse
                    {
                        ContentId = orc.Id,
                        LanguageId = orc.LanguageId,
                        Status = orc.Status.GetDisplayName(),
                        HasDraft = orc.Versions.Any(x => x.IsDraft),
                        HasPublished = orc.Versions.Any(x => x.IsPublished)
                    }),
                AssociatedResources =
                    rc.Resource.AssociatedResourceChildren.Select(ar =>
                        new AssociatedContentResponse
                        {
                            // Get the associated resource content for the current content's language or fallback to English
                            ResourceContent =
                                ar.ResourceContents.OrderByDescending(rci =>
                                    rci.LanguageId == rc.LanguageId ? 2 : rci.LanguageId == englishLanguageId ? 1 : 0).FirstOrDefault(),
                            EnglishLabel = ar.EnglishLabel,
                            ParentResourceName = ar.ParentResource.DisplayName,
                            MediaTypes = ar.ResourceContents.Select(arrc => arrc.MediaType)
                        }),
                ProjectEntity = rc.Projects.FirstOrDefault(),
                ContentVersions =
                    rc.Versions.Select(v => new VersionResponse
                    {
                        Id = v.Id,
                        IsPublished = v.IsPublished,
                        IsDraft = v.IsDraft,
                        Version = v.Version,
                        ContentValue = v.Content,
                        ContentSize = v.ContentSize,
                        WordCount = v.WordCount,
                        DisplayName = v.DisplayName,
                        AssignedUser =
                            v.AssignedUser == null
                                ? null
                                : new UserResponse
                                {
                                    Id = v.AssignedUser.Id,
                                    Name = $"{v.AssignedUser.FirstName} {v.AssignedUser.LastName}",
                                    CompanyId = v.AssignedUser.CompanyId
                                }
                    })
            }).FirstOrDefaultAsync(ct);

        if (resourceContent?.ContentVersions.Any() != true)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        resourceContent.VerseReferences = await dbContext.VerseResources
            .Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(vr => new VerseReferenceResponse { VerseId = vr.VerseId }).ToListAsync(ct);

        resourceContent.PassageReferences = await dbContext.PassageResources
            .Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(pr => new PassageReferenceResponse { StartVerseId = pr.Passage.StartVerseId, EndVerseId = pr.Passage.EndVerseId })
            .ToListAsync(ct);

        var contentVersions = resourceContent.ContentVersions.Where(x => x.IsPublished || x.IsDraft).ToList();
        if (contentVersions.Count == 0)
        {
            contentVersions = resourceContent.ContentVersions.OrderByDescending(x => x.Version).Take(1).ToList();
        }

        resourceContent.ContentVersions = contentVersions;

        await SendOkAsync(resourceContent, ct);
    }
}