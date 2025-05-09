using Aquifer.Common.Extensions;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Versions.Get.StatusHistory;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/versions/{VersionId}/status-history");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var statusHistory = await dbContext.ResourceContentVersionStatusHistory
            .Where(h => h.ResourceContentVersionId == request.VersionId)
            .Select(h => new Response
            {
                Event = h.Status.GetDisplayName(),
                DateOfEvent = h.Created,
            })
            .ToListAsync(ct);

        var userHistory = await dbContext.ResourceContentVersionAssignedUserHistory
            .Where(h => h.ResourceContentVersionId == request.VersionId && h.AssignedUser != null)
            .Select(h => new Response
            {
                Event = $"{h.AssignedUser!.FirstName} {h.AssignedUser.LastName}",
                DateOfEvent = h.Created,
            })
            .ToListAsync(ct);

        Response = [.. statusHistory.Concat(userHistory).OrderByDescending(h => h.DateOfEvent)];
    }
}