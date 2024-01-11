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
                Status = ResourceContentStatus.TranslationNotStarted,
                Trusted = true,
                Versions = [newResourceContentVersion]
            },
            cancellationToken);

        var user = await userService.GetUserFromJwtAsync(cancellationToken);
        await historyService.AddStatusHistoryAsync(newResourceContentVersion,
            ResourceContentStatus.TranslationNotStarted,
            user.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
        return TypedResults.Created();
    }
}