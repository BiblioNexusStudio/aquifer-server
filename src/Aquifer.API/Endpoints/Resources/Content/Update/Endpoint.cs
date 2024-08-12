using System.Text;
using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Update;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Patch("/resources/content/{ContentId}");
        Permissions(PermissionName.EditContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var entity = await dbContext.ResourceContentVersions.Where(x => x.IsDraft)
            .Where(x => x.ResourceContentId == request.ContentId)
            .Include(x => x.ResourceContent)
            .SingleOrDefaultAsync(ct);

        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await userService.GetUserFromJwtAsync(ct);
        var changesMade = ChangesMade(request, entity);

        if (user.Id != entity.AssignedUserId && changesMade)
        {
            ThrowError("Not allowed to edit this resource");
        }

        if (request.Content is not null)
        {
            entity.Content = JsonUtilities.DefaultSerialize(request.Content);
            entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);
        }

        if (request.WordCount is not null)
        {
            entity.WordCount = request.WordCount;
        }

        if (request.DisplayName is not null)
        {
            entity.DisplayName = request.DisplayName;
        }

        if (changesMade)
        {
            entity.ResourceContent.ContentUpdated = DateTime.Now;
            await dbContext.ResourceContentVersionEditTimes.AddAsync(new ResourceContentVersionEditTimeEntity
                {
                    UserId = user.Id,
                    ResourceContentVersionId = entity.Id
                },
                ct);
        }

        await dbContext.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }

    private bool ChangesMade(Request request, ResourceContentVersionEntity currentVersion)
    {
        if (request.DisplayName != currentVersion.DisplayName)
        {
            return true;
        }

        // TODO: once our word counting is fixed this should be put back
        // if (request.WordCount != currentVersion.WordCount)
        // {
        //     return true;
        // }

        if (request.Content is null)
        {
            return false;
        }

        var tiptapType = currentVersion.ResourceContent.MediaType == ResourceContentMediaType.Text
            ? TiptapContentType.Html
            : TiptapContentType.None;
        var currentHtml = TiptapUtilities.ConvertFromJson(currentVersion.Content, tiptapType);

        var newHtml = TiptapUtilities.ConvertFromJson(JsonUtilities.DefaultSerialize(request.Content), tiptapType);

        if (currentHtml is IEnumerable<string> currentHtmlStrings && newHtml is IEnumerable<string> newHtmlStrings)
        {
            return !currentHtmlStrings.SequenceEqual(newHtmlStrings);
        }

        return currentHtml != newHtml;
    }
}