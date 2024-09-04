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
            var subscriberToDisable = await dbContext.ContentSubscribers.SingleOrDefaultAsync(s => s.UnsubscribeId == req.UnsubscribeId, ct);
            if (subscriberToDisable is not null)
            {
                subscriberToDisable.Enabled = false;
                await dbContext.SaveChangesAsync(ct);
            }

            await SendOkAsync("You have been successfully unsubscribed.", ct);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred. Please try again");
        }
    }
}