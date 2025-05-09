using Aquifer.Common.Extensions;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.Snapshots.Get;

public class Endpoint(AquiferDbContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/content/snapshots/{SnapshotId}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var snapshot = await dbContext.ResourceContentVersionSnapshots.SingleOrDefaultAsync(rcvs => rcvs.Id == request.SnapshotId, ct);

        if (snapshot is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(
            new Response
            {
                Id = snapshot.Id,
                DisplayName = snapshot.DisplayName,
                WordCount = snapshot.WordCount,
                Created = snapshot.Created,
                AssignedUserName = snapshot.User == null ? null : $"{snapshot.User.FirstName} {snapshot.User.LastName}",
                Status = snapshot.Status.GetDisplayName(),
                ContentValue = snapshot.Content,
            },
            ct);
    }
}