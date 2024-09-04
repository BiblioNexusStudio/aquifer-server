using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Marketing.Unsubscribe;

public class Endpoint(AquiferDbContext dbContext, ILogger<Endpoint> logger) : Endpoint<Request>
{
    public override void Configure()
    {
        Get("/marketing/unsubscribe/{UnsubscribeId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            await dbContext.ContentSubscribers.Where(s => s.UnsubscribeId == req.UnsubscribeId)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.Enabled, false), ct);
            await SendOkAsync("You have been successfully unsubscribed.", ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to handle subscriber request: {requestContent}", JsonUtilities.DefaultSerialize(req));
            await SendOkAsync("An error occurred. Please try again later", ct);
        }
    }
}