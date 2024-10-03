using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.CreateTranslation;

public class Endpoint(AquiferDbContext dbContext, IUserService userService, IResourceHistoryService historyService)
    : Endpoint<Request, Response>
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

        if (isCommunityUser) {
            var isUserAssigned = await dbContext.ResourceContentVersions
                .AnyAsync(x => x.AssignedUserId == user.Id, ct);

            if (isUserAssigned) {
                ThrowError("User can only create one translation at a time");
            }
        }

        var baseContent = await dbContext.ResourceContents.Where(x => x.Id == request.BaseContentId)
            .Include(x => x.Versions)
            .SingleOrDefaultAsync(ct);
        if (baseContent is null ||
            !baseContent.Versions.Any(x => x.IsPublished) ||
            (request.UseDraft && !baseContent.Versions.Any(x => x.IsDraft)))
        {
            ThrowError("Base version not found");
        }

        var isExistingTranslation = await dbContext.ResourceContents.AnyAsync(x =>
                x.LanguageId == request.LanguageId && x.ResourceId == baseContent.ResourceId,
            ct);
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
            Version = 1
        };

        if (isCommunityUser) {
            newResourceContentVersion.AssignedUserId = user.Id;
        }

        var newResourceContent = new ResourceContentEntity
        {
            LanguageId = language.Id,
            ResourceId = baseContent.ResourceId,
            MediaType = baseContent.MediaType,
            Status = isCommunityUser 
                ? ResourceContentStatus.TranslationInProgress 
                : ResourceContentStatus.TranslationNotStarted,
            Trusted = true,
            Versions = [newResourceContentVersion]
        };

        await dbContext.ResourceContents.AddAsync(newResourceContent, ct);
        await historyService.AddStatusHistoryAsync(
            newResourceContentVersion, 
            newResourceContent.Status,
            user.Id, 
            ct
        );

        if (isCommunityUser) {
            await historyService.AddSnapshotHistoryAsync(
                newResourceContentVersion, 
                user.Id, 
                ResourceContentStatus.TranslationInProgress, 
                ct);
            await historyService.AddAssignedUserHistoryAsync(newResourceContentVersion, user.Id, user.Id, ct);
        }

        await dbContext.SaveChangesAsync(ct);
        await SendOkAsync(new Response(newResourceContent.Id), ct);
    }
}