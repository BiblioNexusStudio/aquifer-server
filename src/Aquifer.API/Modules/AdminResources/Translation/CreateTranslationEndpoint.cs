using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.AdminResources.Translation;

public static class CreateTranslationEndpoint
{
    public const string Path = "content/create-translation";

    public static async Task<Results<Created, BadRequest<string>>> Handle(
        [FromBody] CreateTranslationRequest request,
        AquiferDbContext dbContext,
        IResourceHistoryService historyService,
        IUserService userService,
        CancellationToken ct)
    {
        var baseContent = await dbContext.ResourceContents.Where(x => x.Id == request.BaseContentId)
            .Include(x => x.Versions)
            .SingleOrDefaultAsync(ct);
        if (baseContent is null ||
            !baseContent.Versions.Any(x => x.IsPublished) ||
            (request.UseDraft && !baseContent.Versions.Any(x => x.IsDraft)))
        {
            return TypedResults.BadRequest("Base version not found");
        }

        var isExistingTranslation = await dbContext.ResourceContents.AnyAsync(x =>
                x.LanguageId == request.LanguageId && x.ResourceId == baseContent.ResourceId,
            ct);
        if (isExistingTranslation)
        {
            return TypedResults.BadRequest("Translation already exists");
        }

        var language = await dbContext.Languages.FindAsync([request.LanguageId], ct);
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

        var newResourceContent = new ResourceContentEntity
        {
            LanguageId = language.Id,
            ResourceId = baseContent.ResourceId,
            MediaType = baseContent.MediaType,
            Status = ResourceContentStatus.TranslationNotStarted,
            Trusted = true,
            Versions = [newResourceContentVersion]
        };

        await dbContext.ResourceContents.AddAsync(newResourceContent, ct);

        var user = await userService.GetUserFromJwtAsync(ct);
        await historyService.AddStatusHistoryAsync(newResourceContentVersion,
            ResourceContentStatus.TranslationNotStarted,
            user.Id,
            ct);

        await dbContext.SaveChangesAsync(ct);
        return TypedResults.Created();
    }
}