using Aquifer.API.Common;
using Aquifer.API.Modules.AdminResources.Aquiferization;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Translation;

public static class TranslationEndpoints
{
    public static async Task<Results<Ok, BadRequest<string>>> AssignTranslator(int contentId,
        [FromBody] AquiferizationRequest request,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var translationDraft = await dbContext.ResourceContentVersions
            .Where(rcv => rcv.ResourceContentId == contentId && rcv.IsDraft).Include(rcv => rcv.ResourceContent)
            .SingleOrDefaultAsync(cancellationToken);

        if (translationDraft?.ResourceContent is null)
        {
            return TypedResults.BadRequest("Resource content not found or not in draft status");
        }

        if (!await userService.ValidateNonNullUserIdAsync(request.AssignedUserId, cancellationToken))
        {
            return TypedResults.BadRequest(AdminResourcesHelpers.InvalidUserIdResponse);
        }

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        bool hasAssignOverridePermission = userService.HasJwtClaim(PermissionName.AssignOverride);
        if ((!hasAssignOverridePermission && translationDraft.AssignedUserId != user.Id) ||
            translationDraft.AssignedUserId == request.AssignedUserId)
        {
            return TypedResults.BadRequest("Unable to assign user");
        }

        await historyService.AddAssignedUserHistoryAsync(translationDraft.Id, request.AssignedUserId, user.Id);
        if (translationDraft.ResourceContent.Status != ResourceContentStatus.TranslateInProgress)
        {
            translationDraft.ResourceContent.Status = ResourceContentStatus.TranslateInProgress;
            await historyService.AddStatusHistoryAsync(translationDraft.Id,
                ResourceContentStatus.TranslateInProgress,
                user.Id);
        }

        translationDraft.AssignedUserId = request.AssignedUserId;
        translationDraft.Updated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok();
    }

    public static async Task<Results<Created, BadRequest<string>>> CreateTranslation(
        [FromBody] CreateTranslationRequest request,
        AquiferDbContext dbContext,
        IAdminResourceHistoryService historyService,
        IUserService userService,
        CancellationToken cancellationToken)
    {
        var baseContent = await dbContext.ResourceContents.Where(x => x.Id == request.BaseContentId)
            .Include(x => x.Versions)
            .SingleOrDefaultAsync(cancellationToken);
        if (baseContent is null ||
            !baseContent.Versions.Any(x => x.IsPublished) ||
            (request.UseDraft && !baseContent.Versions.Any(x => x.IsDraft)))
        {
            return TypedResults.BadRequest("Base version not found");
        }

        bool isExistingTranslation = await dbContext.ResourceContents.AnyAsync(x =>
                x.LanguageId == request.LanguageId && x.ResourceId == baseContent.ResourceId,
            cancellationToken);
        if (isExistingTranslation)
        {
            return TypedResults.BadRequest("Translation already exists");
        }

        var language = await dbContext.Languages.FindAsync(request.LanguageId);
        if (language is null)
        {
            return TypedResults.BadRequest("Invalid language id");
        }

        var baseVersion = request.UseDraft
            ? baseContent.Versions.Single(x => x.IsDraft)
            : baseContent.Versions.Single(x => x.IsPublished);

        var newResourceContentVersion = new ResourceContentVersionEntity
        {
            IsPublished = false,
            IsDraft = true,
            DisplayName = baseVersion.DisplayName,
            Content = baseVersion.Content,
            ContentSize = baseVersion.ContentSize,
            WordCount = baseVersion.WordCount,
            Version = 1
        };

        await dbContext.ResourceContents.AddAsync(new ResourceContentEntity
            {
                LanguageId = language.Id,
                ResourceId = baseContent.ResourceId,
                MediaType = baseContent.MediaType,
                Status = ResourceContentStatus.TranslateNotStarted,
                Trusted = true,
                Versions = [newResourceContentVersion]
            },
            cancellationToken);

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        await historyService.AddStatusHistoryAsync(newResourceContentVersion,
            ResourceContentStatus.TranslateNotStarted,
            user.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Created();
    }
}