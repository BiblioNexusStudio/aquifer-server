using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.CreateTranslation;

public class Endpoint(
    AquiferDbContext dbContext,
    IUserService userService,
    IResourceHistoryService historyService,
    ITranslationMessagePublisher translationMessagePublisher) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/resources/content/{BaseContentId}/create-translation");
        Permissions(PermissionName.CreateContent, PermissionName.CreateCommunityContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var isCommunityUser = !userService.HasPermission(PermissionName.CreateContent);

        if (isCommunityUser)
        {
            var isUserAssigned = await dbContext.ResourceContentVersions.AsTracking().AnyAsync(x => x.AssignedUserId == user.Id, ct);

            if (isUserAssigned)
            {
                AddError("User can only create one translation at a time");
                await SendErrorsAsync(cancellation: ct);
                return;
            }
        }

        var baseContent = await dbContext.ResourceContents
            .AsTracking()
            .Where(x => x.Id == request.BaseContentId)
            .Include(x => x.Versions)
            .Include(x => x.Resource)
            .ThenInclude(x => x.ParentResource)
            .SingleOrDefaultAsync(ct);
        if (baseContent is null ||
            !baseContent.Versions.Any(x => x.IsPublished) ||
            (request.UseDraft && !baseContent.Versions.Any(x => x.IsDraft)))
        {
            ThrowError("Base version not found");
        }

        if (baseContent.MediaType != ResourceContentMediaType.Text)
        {
            ThrowError("Cannot translate media type that isn't Text");
        }

        if (isCommunityUser && !baseContent.Resource.ParentResource.AllowCommunityReview)
        {
            ThrowError("Community reviewer is not allowed to translate this resource type");
        }

        var isExistingTranslation = await dbContext.ResourceContents
            .AsTracking()
            .AnyAsync(x => x.LanguageId == request.LanguageId && x.ResourceId == baseContent.ResourceId, ct);
        if (isExistingTranslation)
        {
            ThrowError("Translation already exists");
        }

        var language = await dbContext.Languages.FindAsync([request.LanguageId], ct);
        if (language is null)
        {
            ThrowError("Invalid language id");
        }

        var baseVersion = request.UseDraft ? baseContent.Versions.Single(x => x.IsDraft) : baseContent.Versions.Single(x => x.IsPublished);
        var originalStatus = baseVersion.ResourceContent.Status;

        var hasFullCreateContentPermission = userService.HasPermission(PermissionName.CreateContent);

        var newResourceContentVersion = new ResourceContentVersionEntity
        {
            IsPublished = false,
            IsDraft = true,
            ReviewLevel =
                hasFullCreateContentPermission
                    ? ResourceContentVersionReviewLevel.Professional
                    : ResourceContentVersionReviewLevel.Community,
            DisplayName = baseVersion.DisplayName,
            Content = baseVersion.Content,
            ContentSize = baseVersion.ContentSize,
            WordCount = baseVersion.WordCount,
            SourceWordCount = baseVersion.WordCount,
            Version = 1,

            // Assign the resource immediately even though translation has not yet started
            // to ensure that Community Reviewers only have a single assigned resource.
            // This will also apply to other callers of this endpoint (Publishers) for consistency/simplicity.
            AssignedUserId = user.Id
        };

        var newResourceContent = new ResourceContentEntity
        {
            SourceLanguageId = baseContent.LanguageId,
            LanguageId = language.Id,
            ResourceId = baseContent.ResourceId,
            MediaType = baseContent.MediaType,
            Status = ResourceContentStatus.TranslationAwaitingAiDraft,
            Trusted = true,
            Versions = [newResourceContentVersion]
        };

        await dbContext.ResourceContents.AddAsync(newResourceContent, ct);

        await historyService.AddStatusHistoryAsync(newResourceContentVersion, newResourceContent.Status, user.Id, ct);

        await historyService.AddSnapshotHistoryAsync(newResourceContentVersion, user.Id, originalStatus, ct);

        await historyService.AddAssignedUserHistoryAsync(newResourceContentVersion, user.Id, user.Id, ct);

        await dbContext.SaveChangesAsync(ct);

        await translationMessagePublisher.PublishTranslateResourceMessageAsync(
            new TranslateResourceMessage(
                newResourceContent.Id,
                user.Id,
                false,
                isCommunityUser ? TranslationOrigin.CommunityReviewer : TranslationOrigin.CreateTranslation),
            ct);

        await SendOkAsync(new Response(newResourceContent.Id), ct);
    }
}