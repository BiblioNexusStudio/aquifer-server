using System.Text;
using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Update;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request>
{
    public override void Configure()
    {
        Put("/admin/resources/summary/content/{ContentId}", "/resources/content/{ContentId}");
        Permissions(PermissionName.EditContent);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var entity = await dbContext.ResourceContentVersions
            .Where(x => x.IsDraft)
            .Where(x => x.ResourceContentId == request.ContentId)
            .SingleOrDefaultAsync(ct);

        if (entity is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var user = await userService.GetUserFromJwtAsync(ct);

        if (user.Id != entity.AssignedUserId)
        {
            ThrowError("Not allowed to edit this resource");
        }

        if (request.Content is not null)
        {
            entity.Content = JsonUtilities.DefaultSerialize(request.Content);
        }

        if (request.WordCount is not null)
        {
            entity.WordCount = request.WordCount;
        }

        entity.DisplayName = request.DisplayName;
        entity.ContentSize = Encoding.UTF8.GetByteCount(entity.Content);

        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}