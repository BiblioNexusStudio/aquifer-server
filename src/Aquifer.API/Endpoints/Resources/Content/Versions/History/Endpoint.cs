using Aquifer.Common.Extensions;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Aquifer.API.Endpoints.Resources.Content.Versions.History;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/versions/history/{VersionId}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var statusHistory = await dbContext.ResourceContentVersionStatusHistory
            .Where(h => h.ResourceContentVersionId == request.VersionId)
            .Select(h => new Response
            {
                Event = h.Status.GetDisplayName(),
                DateOfEvent = h.Created
            })
            .ToListAsync(ct);

        var userHistory = await dbContext.ResourceContentVersionAssignedUserHistory
            .Where(h => h.ResourceContentVersionId == request.VersionId)
            .Select(h => new Response
            {
                Event = h.AssignedUser != null ? $"{h.AssignedUser.FirstName} {h.AssignedUser.LastName}" : "",
                DateOfEvent = h.Created
            })
            .ToListAsync(ct);

        userHistory.RemoveAll(h => h.Event.IsNullOrEmpty());
        Response = [.. statusHistory.Concat(userHistory).OrderBy(h => h.DateOfEvent)];
    }
}