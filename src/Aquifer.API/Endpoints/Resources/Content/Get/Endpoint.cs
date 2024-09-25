using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Get;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/{Id}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var self = await userService.GetUserFromJwtAsync(ct);
        var englishLanguageId = await dbContext.Languages.Where(l => l.ISO6393Code == "eng").Select(l => l.Id).SingleAsync(ct);
        var resourceContent = await dbContext.ResourceContents.Where(x => x.Id == request.Id)
            .Select(rc => new Response
            {
                EnglishLabel = rc.Resource.EnglishLabel,
                ParentResourceName = rc.Resource.ParentResource.DisplayName,
                ParentResourceLicenseInfo =
                    rc.Resource.ParentResource.LicenseInfo == null
                        ? null
                        : JsonUtilities.DefaultDeserialize(rc.Resource.ParentResource.LicenseInfo),
                ResourceContentId = rc.Id,
                ResourceId = rc.ResourceId,
                Language = new LanguageResponse
                {
                    Id = rc.LanguageId,
                    EnglishDisplay = rc.Language.EnglishDisplay,
                    ISO6393Code = rc.Language.ISO6393Code,
                    ScriptDirection = rc.Language.ScriptDirection
                },
                Status = rc.Status,
                MediaType = rc.MediaType,
                ContentTranslations = rc.Resource.ResourceContents
                    .Where(orc => orc.MediaType == rc.MediaType)
                    .Select(orc => new TranslationResponse
                    {
                        ContentId = orc.Id,
                        LanguageId = orc.LanguageId,
                        Status = orc.Status.GetDisplayName(),
                        HasDraft = orc.Versions.Any(x => x.IsDraft),
                        HasPublished = orc.Versions.Any(x => x.IsPublished),
                        ResourceContentStatus = orc.Status
                    }),
                AssociatedResources = rc.Resource.AssociatedResourceChildren.Select(ar => new AssociatedContentResponse
                {
                    // Get the associated resource content for the current content's language or fallback to English
                    ResourceContent =
                        ar.ResourceContents.Where(rci => rci.MediaType == ResourceContentMediaType.Text)
                            .OrderByDescending(rci => rci.LanguageId == rc.LanguageId ? 2 : rci.LanguageId == englishLanguageId ? 1 : 0)
                            .FirstOrDefault(),
                    EnglishLabel = ar.EnglishLabel,
                    ParentResourceName = ar.ParentResource.DisplayName,
                    MediaTypes = ar.ResourceContents.Select(arrc => arrc.MediaType)
                }),
                ProjectEntity = rc.Projects.FirstOrDefault(),
                HasPublishedVersion = rc.Versions.Any(rcv => rcv.IsPublished)
            })
            .AsSplitQuery()
            .FirstOrDefaultAsync(ct);

        if (resourceContent is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        resourceContent.AssociatedResources = resourceContent.AssociatedResources.OrderBy(x => x.EnglishLabel);

        resourceContent.VerseReferences = await dbContext.VerseResources.Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(vr => new VerseReferenceResponse { VerseId = vr.VerseId })
            .ToListAsync(ct);

        resourceContent.PassageReferences = await dbContext.PassageResources.Where(x => x.ResourceId == resourceContent.ResourceId)
            .Select(pr => new PassageReferenceResponse { StartVerseId = pr.Passage.StartVerseId, EndVerseId = pr.Passage.EndVerseId })
            .ToListAsync(ct);

        var relevantContentVersion = await dbContext.ResourceContentVersions.Where(rcv => rcv.ResourceContentId == request.Id)
            .Include(rcv => rcv.CommentThreads)
            .ThenInclude(cth => cth.CommentThread.Comments)
            .ThenInclude(c => c.User)
            .Include(rcv => rcv.CommentThreads)
            .ThenInclude(cth => cth.CommentThread.ResolvedByUser)
            .Include(rcv => rcv.AssignedUser)
            .Include(rcv => rcv.MachineTranslations)
            .OrderBy(rcv => rcv.IsDraft ? 0 : rcv.IsPublished ? 1 : 2)
            .ThenByDescending(rcv => rcv.Version)
            .FirstOrDefaultAsync(ct);

        if (relevantContentVersion is null)
        {
            ThrowError("Data integrity issue, no resource content version found.");
        }

        if (relevantContentVersion.IsDraft &&
            relevantContentVersion.AssignedUserId != self.Id &&
            userService.HasPermission(PermissionName.SendReviewContent) &&
            Constants.ReviewPendingStatuses.Contains(resourceContent.Status))
        {
            resourceContent.CanPullBackToManagerReview = resourceContent.ProjectEntity?.CompanyLeadUserId == self.Id ||
                                                         await dbContext.ResourceContentVersionAssignedUserHistory
                                                             .Where(h => h.ResourceContentVersionId == relevantContentVersion.Id &&
                                                                         h.AssignedUserId != null)
                                                             .OrderByDescending(h => h.Created)
                                                             .Select(h => h.AssignedUserId == self.Id)
                                                             .FirstOrDefaultAsync(ct);
        }

        var snapshots = await dbContext.ResourceContentVersionSnapshots
            .Where(rcvs => rcvs.ResourceContentVersionId == relevantContentVersion.Id)
            .OrderByDescending(rcvs => rcvs.Created)
            .Select(rcvs => new SnapshotResponse
            {
                Id = rcvs.Id,
                Created = rcvs.Created,
                AssignedUserName = rcvs.User == null ? null : $"{rcvs.User.FirstName} {rcvs.User.LastName}",
                Status = rcvs.Status.GetDisplayName()
            })
            .ToListAsync(ct);

        var versions = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.Id != relevantContentVersion.Id && rcv.ResourceContentId == relevantContentVersion.ResourceContentId)
            .OrderByDescending(rcv => rcv.Created)
            .Select(rcv => new VersionResponse { Id = rcv.Id, Created = rcv.Created, Version = rcv.Version, IsPublished = rcv.IsPublished })
            .ToListAsync(ct);

        resourceContent.MachineTranslations = relevantContentVersion.MachineTranslations.Select(mt => new MachineTranslationResponse
        {
            Id = mt.Id,
            ContentIndex = mt.ContentIndex,
            UserId = mt.UserId,
            UserRating = mt.UserRating,
            ImproveClarity = mt.ImproveClarity,
            ImproveConsistency = mt.ImproveConsistency,
            ImproveTone = mt.ImproveTone
        })
            .ToList();

        // TODO: remove after deploy
        resourceContent.MachineTranslation = relevantContentVersion.MachineTranslations.Select(mt => new MachineTranslationResponse
        {
            Id = mt.Id,
            UserId = mt.UserId,
            UserRating = mt.UserRating,
            ImproveClarity = mt.ImproveClarity,
            ImproveConsistency = mt.ImproveConsistency,
            ImproveTone = mt.ImproveTone
        })
            .FirstOrDefault();

        resourceContent.IsDraft = relevantContentVersion.IsDraft;
        resourceContent.ResourceContentVersionId = relevantContentVersion.Id;
        resourceContent.ContentValue = relevantContentVersion.Content;
        resourceContent.ContentSize = relevantContentVersion.ContentSize;
        resourceContent.DisplayName = relevantContentVersion.DisplayName;
        resourceContent.WordCount = relevantContentVersion.WordCount;
        resourceContent.Snapshots = snapshots;
        resourceContent.Versions = versions;

        if (resourceContent.IsDraft)
        {
            resourceContent.CommentThreads = new CommentThreadsResponse
            {
                ThreadTypeId = relevantContentVersion.Id,
                Threads = relevantContentVersion.CommentThreads.Select(x => new ThreadResponse
                {
                    Id = x.CommentThreadId,
                    Resolved = x.CommentThread.Resolved,
                    Comments = x.CommentThread.Comments.Select(c => new CommentResponse
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        User = UserDto.FromUserEntity(c.User)!,
                        DateTime = c.Updated
                    })
                            .ToList()
                })
                    .ToList()
            };

            if (relevantContentVersion.AssignedUser is not null)
            {
                resourceContent.AssignedUser = new UserResponse
                {
                    Id = relevantContentVersion.AssignedUser.Id,
                    Name = $"{relevantContentVersion.AssignedUser.FirstName} {relevantContentVersion.AssignedUser.LastName}",
                    CompanyId = relevantContentVersion.AssignedUser.CompanyId
                };
            }
        }

        await SendOkAsync(resourceContent, ct);
    }
}