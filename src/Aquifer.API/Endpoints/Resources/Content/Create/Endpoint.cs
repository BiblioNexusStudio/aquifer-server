using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    private const string EmptyTipTapContent = "[{\"tiptap\":{\"type\":\"doc\",\"content\":[{\"type\":\"paragraph\"}]}}]";

    public override void Configure()
    {
        Post("/resources/content");
        Permissions(PermissionName.CreateContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var titleAlreadyExist = await dbContext.ResourceContentVersions.AnyAsync(x =>
                ((request.LanguageId > 1 && request.LanguageTitle != null && x.DisplayName == request.LanguageTitle) ||
                 x.ResourceContent.Resource.EnglishLabel == request.EnglishLabel) &&
                x.ResourceContent.Resource.ParentResourceId == request.ParentResourceId,
            ct);

        if (titleAlreadyExist)
        {
            await SendAsync("Content with the same Title already exists. Please change the title.", 400, ct);
            return;
        }

        var currentUser = await userService.GetUserFromJwtAsync(ct);

        var resource = new ResourceEntity
        {
            EnglishLabel = request.EnglishLabel,
            ParentResourceId = request.ParentResourceId,
            SortOrder = 0
        };

        var resourceContentEntity = new ResourceContentEntity
        {
            Resource = resource,
            LanguageId = request.LanguageId,
            Trusted = true,
            Status = ResourceContentStatus.AquiferizeEditorReview,
            MediaType = ResourceContentMediaType.Text
        };

        var resourceContentVersionEntity = new ResourceContentVersionEntity
        {
            ResourceContent = resourceContentEntity,
            DisplayName = request.LanguageTitle ?? request.EnglishLabel,
            AssignedUserId = currentUser.Id,
            Version = 1,
            IsDraft = true,
            IsPublished = false,
            Content = EmptyTipTapContent,
            ResourceContentVersionStatusHistories =
            [
                new ResourceContentVersionStatusHistoryEntity
                {
                    Status = ResourceContentStatus.AquiferizeEditorReview,
                    ChangedByUserId = currentUser.Id
                }
            ],
            ResourceContentVersionAssignedUserHistories =
            [
                new ResourceContentVersionAssignedUserHistoryEntity
                {
                    AssignedUserId = currentUser.Id,
                    ChangedByUserId = currentUser.Id
                }
            ]
        };

        await dbContext.ResourceContentVersions.AddAsync(resourceContentVersionEntity, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}