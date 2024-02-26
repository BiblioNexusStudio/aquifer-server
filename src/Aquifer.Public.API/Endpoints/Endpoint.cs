using Aquifer.Common.Tiptap;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/test");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // var rt = (await dbContext.ResourceContentVersions.Where(x => x.Id == 38464).Include(x => x.ResourceContentVersionStatusHistories)
        //     .ToListAsync(ct)).Select(x => new { test = x.ResourceContentVersionStatusHistories.FirstOrDefault()?.Id });

        var r = await dbContext.ResourceContentVersions.SingleAsync(x => x.Id == 38464, ct);

        var des = TiptapUtilities.ConvertFromJson(r.Content, TiptapContentType.Markdown);

        await SendStringAsync(null, cancellation: ct);
    }
}